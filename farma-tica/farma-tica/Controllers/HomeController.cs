using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace farma_tica.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAlgo()
        {
            return Json(new List<string>() {"hola", "hola de nuevo"},JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public JsonResult GetAlgo(Model argumento)
        //{
        //    return Json(new List<string>() { "hola", "hola de nuevo" });
        //}
    }
}