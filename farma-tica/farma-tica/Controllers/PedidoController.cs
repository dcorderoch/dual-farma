using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class PedidoController : Controller
    {
        //CRUD

        // URI from Angular: /home/Pedido/Create
        [HttpPost]
        public JsonResult Create(Order theOrder)
        {
            //MUST CHANGE
            /*
            public int CreateOrderWithoutPrescription(string clientId, string[] medicinesId, string pickupOfficeId,
            string prefPhoneNume, DateTime pickUpdDate, bool type)
            */
            var om = new OrderManager();
            //return Json(new ReturnStatus() {StatusCode = om.CreateOrderWithoutPrescription(theOrder.ClientId,theOrder.)})
            return Json(new ReturnStatus() {StatusCode = 200}, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /home/Pedido/CreateWPrescription
        public JsonResult CreateWPrescription(Order theOrder)
        {
            //MUST CHANGE
            /*
            public int CreateOrderWithoutPrescription(string clientId, string[] medicinesId, string pickupOfficeId,
            string prefPhoneNume, DateTime pickUpdDate, bool type)
            */
            var om = new OrderManager();
            //return Json(new ReturnStatus() {StatusCode = om.CreateOrderWithoutPrescription(theOrder.ClientId,theOrder.)})
            return Json(new ReturnStatus() { StatusCode = 200 }, JsonRequestBehavior.AllowGet);
        }
        // URI from Angular: /home/Pedido/Update
        // URI from Angular: /home/Pedido/Delete
    }
}
