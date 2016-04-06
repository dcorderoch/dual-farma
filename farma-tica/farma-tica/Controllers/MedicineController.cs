using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class MedicineController : Controller
    {
        // URI from Angular: /home/Medicine/Create
        [HttpPost]
        public JsonResult Create(MedicineCreate newMed)
        {
            var medm = new Medicine_Manager();
            return
                Json(new ReturnStatus()
                {
                    StatusCode =
                        medm.CreateMedicine(newMed.name, newMed.requiresPrescription, newMed.price, newMed.originOffice, newMed.stock)
                },JsonRequestBehavior.AllowGet);
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
            return Json(new ReturnStatus() {StatusCode = 500}, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /home/Medicine/Delete
        public JsonResult Delete(MedicineByOffice medToDel)
        {
            var medm = new Medicine_Manager();
            return Json(medm.DeleteMedicine(medToDel.medicineId, medToDel.branchOffice), JsonRequestBehavior.AllowGet);
        }
    }
}
