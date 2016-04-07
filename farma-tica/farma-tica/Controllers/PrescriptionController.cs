using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using farma_tica.BLL;
using farma_tica.DAL.Models;
using farma_tica.Controllers.Models;

namespace farma_tica.Controllers
{
    public class PrescriptionController : ApiController
    {
        // URI from Angular: api/Prescription/New
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

        // URI from Angular: api/Prescription/GetAll
        [HttpGet]
        public JsonResult<List<Prescription>> GetAll()
        {
            var pm = new Prescription_Manager();
            return Json(pm.GetAllPrescriptions());
        }

        // URI from Angular: api/Prescription/Delete
        public JsonResult<ReturnStatus> Delete(IdPrescription prescription)
        {
            var pm = new Prescription_Manager();
            return Json(new ReturnStatus() {StatusCode = pm.DeletePrescription(prescription.pID)});
        }
    }
}