using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class DoctorController : ApiController
    {
        // URI from Angular: /home/Doctor/Create
        [HttpPost]
        public JsonResult<ReturnStatus> Create(Doctor newDoc)
        {
            var docm = new Doctor_Manager();
            return
                Json(new ReturnStatus()
                {
                    StatusCode =
                        docm.CreateDoctor(newDoc.DoctorId, newDoc.IdNumber, newDoc.Name, newDoc.LastName1,
                            newDoc.LastName2, newDoc.PlaceResidence)
                });//,JsonRequestBehavior.AllowGet);
        }
        // URI from Angular: /home/Doctor/GetAll
        [HttpGet]
        public JsonResult<List<Doctor>> GetAll()
        {
            var docm = new Doctor_Manager();
            return Json(docm.GetAllDoctors());//, JsonRequestBehavior.AllowGet);
        }

    }
}