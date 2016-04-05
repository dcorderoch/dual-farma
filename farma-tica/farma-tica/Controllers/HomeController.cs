using System.Collections.Generic;
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
            return Json(new List<string> {"hola", "hola de nuevo"}, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetAlgo(Model argumento)
        //[HttpPost]
        //{
        //    return Json(new List<string>() { "hola", "hola de nuevo" });
        //}
    }
}