using System.Web.Http;
using System.Web.Http.Results;

namespace farma_tica.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public JsonResult<ReturnStatus> Get()
        {
            return Json(new ReturnStatus() { StatusCode = 200 });//, JsonRequestBehavior.AllowGet);
        }
        // GET: Home
        //public ActionResult Index()
        //{
        //return View();
        //}
        

        [HttpGet]
        public JsonResult<ReturnStatus> Test()
        {
            return Json(new ReturnStatus() {StatusCode = 200});//, JsonRequestBehavior.AllowGet);
        }
    }
}