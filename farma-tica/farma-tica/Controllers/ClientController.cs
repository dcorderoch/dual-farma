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
        // URI from Angular: api/Client/New
        [HttpPost]
        public JsonResult<ReturnStatus> New(NewClient newClient)
        {
            var clm = new Client_Manager();
            return Json(
                new ReturnStatus()
                {
                    StatusCode =
                        clm.CreateClient(newClient.Id, newClient.Name, newClient.LastName1, newClient.LastName2,
                            newClient.PenaltiesNumber.ToString(), newClient.PlaceResidence, newClient.MedicalHistory,
                            newClient.BornDate, newClient.PhoneMum, newClient.Password)
                });
        }

        // URI from Angular: api/Client/GetAllClients
        [HttpGet]
        [Route()]
        public JsonResult<List<Client>> GetAllClients()
        {
            var clm = new Client_Manager();
            return Json(clm.GetAllClients());
        }

        // URI from Angular: api/Client/Update
        [HttpPost]
        public JsonResult<ReturnStatus> Update(Client theClient)
        {
            var clm = new Client_Manager();
            return Json(
                new ReturnStatus()
                {
                    StatusCode = clm.UpdateClient(theClient.Id, theClient.Name, theClient.LastName1, theClient.LastName2, theClient.PenaltiesNumber.ToString(), theClient.PlaceResidence, theClient.MedicalHistory, theClient.BornDate.ToString(), theClient.PhoneMum, theClient.Password)
                });
        }

        // URI from Angular: api/Client/Delete
        [HttpPost]
        public JsonResult<ReturnStatus> Delete(IdClient clientID)
        {
            var clm = new Client_Manager();
            return Json(new ReturnStatus() {StatusCode = clm.DeleteClient(clientID.cID)});
        }
    }
}
