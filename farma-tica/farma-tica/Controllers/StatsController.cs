﻿using System.Collections.Generic;
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
    public class StatsController : ApiController
    {
        // URI from Angular: /Stats/SalesPerComp
        [HttpPost]
        public JsonResult<List<Medicine>> SalesPerComp(IdCompany Company)
        {
            var medm = new Medicine_Manager();
            return Json(medm.GetMostSoldMedicinesByCompany(Company.compID));//,JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Stats/NewSales
        [HttpPost]
        public JsonResult<List<Medicine>> NewSales(IdCompany Company)
        {
            var medm = new Medicine_Manager();
            return Json(medm.GetMostSoldByNewSoftware(Company.compID));// JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Stats/TotSalesPerComp
        [HttpPost]
        public JsonResult<ReturnStatus> TotSalesPerComp(IdCompany Company)
        {
            var medm = new Medicine_Manager();
            return Json(new ReturnStatus() {StatusCode = medm.TotalSalesByCompany(Company.compID)});//,JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Stats/GlobalSales
        public JsonResult<List<Medicine>>  GlobalSales()
        {
            var medm = new Medicine_Manager();
            return Json(medm.GlobalMostSoldMedicines());//,JsonRequestBehavior.AllowGet);
        }


    }
}