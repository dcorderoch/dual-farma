using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Net.Http;
using System.Security.AccessControl;

using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class LoginController : Controller
    {
        // URI from Angular: /Login/Login
        [HttpPost]
        public JsonResult Login(LoginData login)
        {
            var acm = new Account_Manager();
            var retVal = acm.AuthorizeLogin(login.ID, login.Pass);
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }
    }
}
