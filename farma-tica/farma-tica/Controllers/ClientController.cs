using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class ClientController : ApiController
    {
        // URI from Angular: /Client/New
        [HttpPost]
        public JsonResult<ReturnStatus> New(Client newClient)
        {
            var clm = new Client_Manager();
            return Json(
                new ReturnStatus()
                {
                    StatusCode =
                        clm.CreateClient(newClient.Id, newClient.Name, newClient.LastName1, newClient.LastName2,
                            newClient.PenaltiesNumber.ToString(), newClient.PlaceResidence, newClient.MedicalHistory,
                            newClient.BornDate.ToString(), newClient.PhoneMum, newClient.Password)
                }); //, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular /api/Client/Crap
        [HttpPost]
        public JsonResult<string> Crap(LoginData what)
        {
            if (what == null || what.ID == null || what.Pass == null)
            {
                return Json(new string(("shit").ToCharArray()));
            }
            return Json(new string(("value:" + what.ID + what.Pass).ToCharArray()));
        }

        // URI from Angular: /Client/GetAllClients
        [HttpGet]
        [Route()]
        public JsonResult<List<Client>> GetAllClients()
        {
            var clm = new Client_Manager();
            return Json(clm.GetAllClients());//, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Client/Update
        [HttpPost]
        public JsonResult<ReturnStatus> Update(Client theClient)
        {
            var clm = new Client_Manager();
            return Json(
                new ReturnStatus()
                {
                    StatusCode = clm.UpdateClient(theClient.Id, theClient.Name, theClient.LastName1, theClient.LastName2, theClient.PenaltiesNumber.ToString(), theClient.PlaceResidence, theClient.MedicalHistory, theClient.BornDate.ToString(), theClient.PhoneMum, theClient.Password)
                });//, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Client/Delete
        [HttpPost]
        public JsonResult<ReturnStatus> Delete(IdClient clientID)
        {
            var clm = new Client_Manager();
            return Json(new ReturnStatus() {StatusCode = clm.DeleteClient(clientID.cID)});//,JsonRequestBehavior.AllowGet);
        }
    }
}
