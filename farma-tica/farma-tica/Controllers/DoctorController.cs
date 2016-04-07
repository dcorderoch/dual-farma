using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using farma_tica.BLL;
using farma_tica.DAL.Models;
using farma_tica.Controllers.Models;

namespace farma_tica.Controllers
{
    public class DoctorController : ApiController
    {
        // URI from Angular: api/Doctor/Create
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
                });
        }
        
        // URI from Angular: api/Doctor/GetAll
        [HttpGet]
        public JsonResult<List<Doctor>> GetAll()
        {
            var docm = new Doctor_Manager();
            return Json(docm.GetAllDoctors());
        }

        // URI from Angular: api/Doctor/Update
        [HttpPost]
        public JsonResult<ReturnStatus> Update(Doctor newInfoDoc)
        {
            var docm = new Doctor_Manager();
            return
                Json(new ReturnStatus()
                {
                    StatusCode =
                        docm.UpdateDoctor(newInfoDoc.DoctorId, newInfoDoc.IdNumber, newInfoDoc.Name,
                            newInfoDoc.LastName1, newInfoDoc.LastName2, newInfoDoc.PlaceResidence)
                });
        }

        //URI from Angular: api/Doctor/Delete
        [HttpPost]
        public JsonResult<ReturnStatus> Delete(IdDoc docId)
        {
            var docm = new Doctor_Manager();
            return Json(new ReturnStatus() {StatusCode = docm.DeleteDoctor(docId.docID)});
        }
    }
}