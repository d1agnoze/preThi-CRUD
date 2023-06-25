using preThi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace preThi.Controllers
{
    public class HomeController : Controller
    {   
        private string connect = global::System.Configuration.ConfigurationManager.ConnectionStrings["demo_sqlsvConnectionString"].ConnectionString;
        DataClasses1DataContext db = new DataClasses1DataContext(global::System.Configuration.ConfigurationManager.ConnectionStrings["demo_sqlsvConnectionString"].ConnectionString);
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

    }
}