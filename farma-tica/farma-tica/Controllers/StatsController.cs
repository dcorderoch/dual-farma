using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using farma_tica.BLL;

namespace farma_tica.Controllers
{
    public class StatsController : Controller
    {
        // URI from Angular: /Stats/SalesPerComp
        [HttpPost]
        public JsonResult SalesPerComp(string Company)
        {
            var medm = new Medicine_Manager();
            return Json(medm.GetMostSoldMedicinesByCompany(Company),JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Stats/NewSales
        [HttpPost]
        public JsonResult NewSales(string Company)
        {
            var medm = new Medicine_Manager();
            return Json(medm.GetMostSoldByNewSoftware(Company), JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Stats/TotSalesPerComp
        [HttpPost]
        public JsonResult TotSalesPerComp(string Company)
        {
            var medm = new Medicine_Manager();
            return Json(new ReturnStatus() {StatusCode = medm.TotalSalesByCompany(Company)},
                JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Stats/GlobalSales
        public JsonResult GlobalSales()
        {
            var medm = new Medicine_Manager();
            return Json(medm.GlobalMostSoldMedicines(),
                JsonRequestBehavior.AllowGet);
        }


    }
}