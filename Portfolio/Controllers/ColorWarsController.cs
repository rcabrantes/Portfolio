using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Controllers
{
    public class ColorWarsController : Controller
    {
        // GET: ColorWars
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestHubLogin()
        {
            return View();
        }

        public PartialViewResult LobbyPartialView()
        {
            return PartialView("LobbyPartial");
        }
    }
}