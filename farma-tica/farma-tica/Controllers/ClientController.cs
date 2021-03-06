﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using farma_tica.BLL;
using farma_tica.DAL.Models;

using farma_tica.Controllers.Models;

namespace farma_tica.Controllers
{
    public class ClientController : ApiController
    {
        // URI from Angular: api/Client/Login
        [HttpPost]
        public JsonResult<List<string>> Login(ClientLoginData cLoginData)
        {
            var clm = new Client_Manager();
            return Json(clm.AuthClientLogin(cLoginData.cID, cLoginData.pPass));
        }

        // URI from Angular: api/Client/New
        [HttpPost]
        public JsonResult<ReturnStatus> New(NewClient newClient)
        {
            var clm = new Client_Manager();
            return Json(
                new ReturnStatus()
                {
                    StatusCode =
                        clm.CreateClient(newClient.NumCed, newClient.Name, newClient.LastName1, newClient.LastName2,
                            newClient.PenaltiesNumber, newClient.PlaceResidence, newClient.MedicalHistory,
                            newClient.BornDate, newClient.PhoneMum, newClient.Password)
                });
        }

        // URI from Angular: api/Client/GetAllClients
        [HttpGet]
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
                    StatusCode = clm.UpdateClient(theClient.NumCed, theClient.Name, theClient.LastName1, theClient.LastName2, theClient.PenaltiesNumber, theClient.PlaceResidence, theClient.MedicalHistory, theClient.BornDate, theClient.PhoneMum, theClient.Password)
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