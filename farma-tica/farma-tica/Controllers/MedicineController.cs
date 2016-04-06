using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
//using System.Web.Mvc;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class MedicineController : ApiController
    {
        // URI from Angular: /Medicine/Create
        [HttpPost]
        public JsonResult<ReturnStatus> Create(MedicineCreate newMed)
        {
            var medm = new Medicine_Manager();
            return
                Json(new ReturnStatus()
                {
                    StatusCode =
                        medm.CreateMedicine(newMed.name, newMed.requiresPrescription, newMed.price, newMed.originOffice, newMed.stock)
                });//,JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Medicine/AllMeds
        [HttpPost]
        public JsonResult<List<Medicine>> AllMeds(IdMed House)
        {
            var medm = new Medicine_Manager();
            return Json(medm.GetAllMedicines(House.mID));//, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Medicine/Update
        [HttpPost]
        public JsonResult<ReturnStatus> Update(Medicine newMed)
        {
            //MUST CHANGE
            return Json(new ReturnStatus() {StatusCode = 500});//, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Medicine/Delete
        public JsonResult<ReturnStatus> Delete(MedicineByOffice medToDel)
        {
            var medm = new Medicine_Manager();
            return Json(new ReturnStatus() { StatusCode = medm.DeleteMedicine(medToDel.medicineId, medToDel.branchOffice)});//, JsonRequestBehavior.AllowGet);
        }
    }
}
