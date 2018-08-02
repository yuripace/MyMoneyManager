using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMoneyManager.Models;

namespace MyMoneyManager.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [ChildActionOnly]
        public ActionResult ElencoMovimenti()
        {
            MovimentisController mc = new MovimentisController();
            //return PartialView("_EmployeeRegistration", model);
          
            return PartialView(mc.Index());
        }
    }
}
