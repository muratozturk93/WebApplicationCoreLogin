using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebApplicationCoreLogin.Models;
using WebApplicationCoreLogin.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace WebApplicationCoreLogin.Controllers
{
    public class AccountController : Controller
    {
		private DatabaseContext db;
		private IConfiguration _configuration;

		public AccountController(DatabaseContext dbcontext,IConfiguration configuration)
		{
			db = dbcontext;
			_configuration = configuration;
		}
		// Dependency injection nesneyı bagımlılıktan kurtarma gibi bir anlamı var üstteki olaya deniliyor...

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				string sifre = _configuration.GetValue<string>("Appsettings:sifre");
				sifre = model.Password + sifre;
				string md5sifre = sifre.MD5();

				User user = db.Users.FirstOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password==md5sifre);

				if (user!=null)
				{
					List<Claim> claims = new List<Claim>();
					claims.Add(new Claim ("Id", user.Id.ToString()));
					claims.Add(new Claim("Name", user.Name ?? string.Empty));
					claims.Add(new Claim("Username", user.Username));
					claims.Add(new Claim(ClaimTypes.Role, user.Role ?? string.Empty));    // Admın mı user mı onu tutuyor...

					ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

					HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));

					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Kullanıcı adı ya da şifre hatalıdır");
				}
			}
			return View(model);
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (db.Users.Any(x=>x.Username.ToLower()==model.Username.ToLower()))
				{
					ModelState.AddModelError(nameof(model.Username), "Bu kullanıcı adı sistemde kayıtlıdır");
					return View(model);
				}



				string sifre = _configuration.GetValue<string>("Appsettings:sifre");

				sifre = model.Password + sifre;

				string md5sifre = sifre.MD5();   // Şifreleme işlemi bu aşamada gerçekleşti.

				User user = new User()
				{
					Username = model.Username,
					Password = md5sifre
				};
				db.Users.Add(user);
				if(db.SaveChanges()==0)
				{
					ModelState.AddModelError("", "Kayıt Eklenemedi");
				}
				else
				{
					return RedirectToAction("Login");
				}
			}
			return View();

           
        }

		[Authorize]
        public IActionResult Profil()
        {
            return View();
        }

        public IActionResult Logout()
        {
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
