using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class ClientController : Controller
    {
        // URI from Angular: /Client/New
        [HttpPost]
        public JsonResult New(Client newClient)
        {
            var clm = new Client_Manager();
            return Json(
                new ReturnStatus()
                {
                    StatusCode = clm.CreateClient(newClient.Id,newClient.Name,newClient.LastName1,newClient.LastName2,newClient.PenaltiesNumber.ToString(),newClient.PlaceResidence,newClient.MedicalHistory,newClient.BornDate.ToString(),newClient.PhoneMum,newClient.Password)
                }, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Client/GetAllClients
        [HttpGet]
        public JsonResult GetAllClients()
        {
            var clm = new Client_Manager();
            return Json(clm.GetAllClients(), JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Client/Update
        [HttpPost]
        public JsonResult Update(Client theClient)
        {
            var clm = new Client_Manager();
            return Json(
                new ReturnStatus()
                {
                    StatusCode = clm.UpdateClient(theClient.Id, theClient.Name, theClient.LastName1, theClient.LastName2, theClient.PenaltiesNumber.ToString(), theClient.PlaceResidence, theClient.MedicalHistory, theClient.BornDate.ToString(), theClient.PhoneMum, theClient.Password)
                }, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Client/Delete
        [HttpPost]
        public JsonResult Delete(string cId)
        {
            var clm = new Client_Manager();
            return Json(new ReturnStatus() {StatusCode = clm.DeleteClient(cId)},JsonRequestBehavior.AllowGet);
        }

    }
}
