using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApplicationCoreLogin.Models;

namespace WebApplicationCoreLogin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*MVC projesi olmas� i�in servis.add k�sm�n� ekl�yoruz eklenmi� tikledi�imiz i�in addrazor k�sm�n� daha sonradan �nd�rd�g�m�z� eklemek �c�n ekled�k.*/           
            
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddDbContext<DatabaseContext>(o => { o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });
			Microsoft.AspNetCore.Authentication.AuthenticationBuilder authenticationBuilder = builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
            {
                o.Cookie.Name = "AuthCookie";
                o.ExpireTimeSpan = TimeSpan.FromDays(1);
                o.SlidingExpiration = false;  // True ded�g�m�zde cook�e 1 gun doldugunda tekrardan surey� uzat�r.
                o.LoginPath = "/Account/Login";
                o.LogoutPath = "/Account/Logout";
                o.AccessDeniedPath = "/Home/AccessDenied";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}