using System.Web.Http;
using System.Web.Http.Results;
using farma_tica.BLL;
using farma_tica.Controllers.Models;

namespace farma_tica.Controllers
{
    public class LoginController : ApiController
    {
        // URI from Angular: api/Login/Login
        [HttpPost]
        public JsonResult<string[]> Login(LoginData login)
        {
            var acm = new Account_Manager();
            var retVal = acm.AuthorizeLogin(login.ID, login.Pass);
            return Json(retVal);
        }
    }
}
