using Schedule.Models;
using Schedule.Models.Entities;

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Schedule.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public JsonResult GetEvents()
		{
			List<EventVM> events;

			using(var db = new DatabaseContext())
			{
				events = db.Events.ToList().Select(dbObject => (EventVM)dbObject).ToList();
			}

			return Json(events, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetUsers()
		{
			List<UserVM> users;

			using(var db = new DatabaseContext())
			{
				users = db.Users.ToList().Select(dbObject => (UserVM)dbObject).ToList();
			}

			return Json(users, JsonRequestBehavior.AllowGet);
		}
	}
}