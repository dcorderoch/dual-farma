using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class UserMgmtController : Controller
    {
        // URI from Angular: /home/UserMgmt/Update
        [HttpPost]
        public JsonResult Update(User newUser)
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

        // URI from Angular: /home/UserMgmt/Delete
        public JsonResult Delete(int uID)
        {
            var acm = new Account_Manager();
            return Json(new ReturnStatus() {StatusCode = acm.DeleteUser(uID.ToString())});

        }
    }
}
