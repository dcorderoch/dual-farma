using System.Web.Mvc;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class MedicineController : Controller
    {
        // URI from Angular: /home/Medicine/Create
        [HttpPost]
        public JsonResult Create(Medicine newMed)
        {
            //MUST CHANGE
            var medm = new Medicine_Manager();
            return
                Json(new ReturnStatus
                {
                    StatusCode =
                        medm.CreateMedicine(newMed.Name, newMed.RequiresPrescription.ToString(), newMed.Price.ToString(),
                            "CHANGE", "HOUSE", newMed.Stock.ToString(), newMed.AmmountSold.ToString())
                }, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /home/Medicine/Medicine
        // returns null if it doesn't exist
        [HttpPost]
        public JsonResult Medicine(string mId)
        {
            var medm = new Medicine_Manager();
            return Json(medm.GetMedicineById(mId), JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /home/Medicine/AllMeds
        [HttpPost]
        public JsonResult AllMeds(string House)
        {
            var medm = new Medicine_Manager();
            return Json(medm.GetAllMedicines(House), JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /home/Medicine/Update
        [HttpPost]
        public JsonResult Update(Medicine newMed)
        {
            //MUST CHANGE
            return Json(new ReturnStatus {StatusCode = 500}, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /home/Medicine/Delete
        public JsonResult Delete(string medId)
        {
            var medm = new Medicine_Manager();
            return Json(medm.DeleteMedicine(medId), JsonRequestBehavior.AllowGet);
        }
    }
}