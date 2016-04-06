using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class PrescriptionController : ApiController
    {
        [HttpPost]
        public JsonResult<ReturnStatus> New(PrescriptionInfo nuevaReceta)
        {
            var pm = new Prescription_Manager();
            return
                Json(new ReturnStatus()
                {
                    StatusCode = pm.CreatePrescription(nuevaReceta.doctorId, nuevaReceta.prescriptionImage)
                });
        }

        [HttpGet]
        public JsonResult<List<Prescription>> GetAll()
        {
            var pm = new Prescription_Manager();
            return Json(pm.GetAllPrescriptions());
        }

        public JsonResult<ReturnStatus> Delete(IdPrescription prescription)
        {
            var pm = new Prescription_Manager();
            return Json(new ReturnStatus() {StatusCode = pm.DeletePrescription(prescription.pID)});
        }
    }
}