using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplicationCoreLogin.Models;
using WebApplicationCoreLogin.Models.ViewModel;

namespace WebApplicationCoreLogin.Controllers
{
	[Authorize(Roles = "admin")]
	public class UserController : Controller
    {
        private DatabaseContext db;
        private IMapper _mapper;
        public UserController(DatabaseContext context,IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            List<User> userlist = db.Users.ToList();   //select * from demek.. Listeyi çekmek demek yani..

            List<UserViewModel> model = userlist.Select(x=>_mapper.Map<UserViewModel>(x)).ToList();


            // Userlist teki herbir elemanı userviewmodel e donustur ve model e at.++++++++++++++++++++


            //foreach (User user in userlist)
            //{
            //    users.Add(new UserViewModel
            //    {
            //        Id = user.Id,
            //        Name=user.Name,
            //        Username=user.Username

            //    });
            //}



            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Create(UserViewModel model)
		{
			return View(model);
		}
	}
}
