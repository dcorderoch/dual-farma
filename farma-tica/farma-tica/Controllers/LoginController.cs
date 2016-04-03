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
        // URI from Angular: /home/Login/Login
        [HttpPost]
        public JsonResult Login(LoginData login)
        {
            var acm = new Account_Manager();
            var retVal = acm.AuthorizeLogin(login.ID, login.Pass);
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /home/Login/New
        [HttpPost]
        public JsonResult New(User newUser)
        {
            var acm = new Account_Manager();
            return
                Json(new ReturnStatus()
                {
                    StatusCode =
                        acm.CreateUser(newUser.IdUsuario, newUser.Password, newUser.Name, newUser.LastName1,
                            newUser.LastName2, newUser.Email, newUser.Company, newUser.RoleId.ToString())
                });
        }

        // URI from Angular: /home/Login/GetAll
        [HttpPost]
        public JsonResult GetAll(string Company)
        {
            var acm = new Account_Manager();
            return Json(acm.GetAllUsers(Company), JsonRequestBehavior.AllowGet);
        }
    }
}
