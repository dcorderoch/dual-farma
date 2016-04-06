using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class StatsController : ApiController
    {
        // URI from Angular: api/Stats/SalesPerComp
        [HttpPost]
        public JsonResult<List<Medicine>> SalesPerComp(IdCompany Company)
        {
            var medm = new Medicine_Manager();
            return Json(medm.GetMostSoldMedicinesByCompany(Company.compID));
        }

        // URI from Angular: api/Stats/NewSales
        [HttpPost]
        public JsonResult<List<Medicine>> NewSales(IdCompany Company)
        {
            var medm = new Medicine_Manager();
            return Json(medm.GetMostSoldByNewSoftware(Company.compID));
        }

        // URI from Angular: api/Stats/TotSalesPerComp
        [HttpPost]
        public JsonResult<ReturnStatus> TotSalesPerComp(IdCompany Company)
        {
            var medm = new Medicine_Manager();
            return Json(new ReturnStatus() {StatusCode = medm.TotalSalesByCompany(Company.compID)});
        }

        // URI from Angular: api/Stats/GlobalSales
        public JsonResult<List<Medicine>>  GlobalSales()
        {
            var medm = new Medicine_Manager();
            return Json(medm.GlobalMostSoldMedicines());
        }


    }
}