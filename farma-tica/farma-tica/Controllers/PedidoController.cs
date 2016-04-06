using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

//using System.Web.Mvc;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class PedidoController : ApiController
    {
        // URI from Angular: /Pedido/Create
        [HttpPost]
        public JsonResult<ReturnStatus> Create(OrderWithoutPrescription theOrder)
        {
            var om = new OrderManager();
            return
                Json(new ReturnStatus()
                {
                    StatusCode =
                        om.CreateOrderWithoutPrescription(theOrder.clientId, theOrder.medicineIds,
                            theOrder.pickupOfficeId, theOrder.prefPhoneNumber, theOrder.pickupDate, theOrder.type)
                });//,JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Pedido/CreateWPrescription
        [HttpPost]
        public JsonResult<ReturnStatus> CreateWPrescription(OrderWithPrescription theOrder)
        {
            var om = new OrderManager();
            return Json(new ReturnStatus()
            {
                StatusCode = om.CreateOrderWithPrescription(
                    theOrder.clientId,theOrder.medicinesId,theOrder.prescriptedMedicinesId,theOrder.prescriptionImage,theOrder.DoctorId,theOrder.pickupOfficeId,theOrder.prefPhoneNumber,theOrder.pickupDate,theOrder.type)
            });//, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Pedido/GetAllByBranchOffice
        [HttpPost]
        public JsonResult<List<Order>> GetAllByBranchOffice(IdBranchOffice branchOfficeId)
        {
            var om = new OrderManager();
            return Json(om.GetAllOrdersByBranchOffice(branchOfficeId.boID));//,JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Pedido/Delete
        [HttpPost]
        public JsonResult<ReturnStatus> Delete(IdOrder orderId)
        {
            var om = new OrderManager();
            return Json(new ReturnStatus() {StatusCode = om.DeleteOrder(orderId.oID)});//, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Pedido/Update
        [HttpPost]
        public JsonResult<ReturnStatus> Update(OrderStatus newStatus)
        {
            var om = new OrderManager();
            return Json(new ReturnStatus() { StatusCode = om.UpdateOrderStatus(newStatus.Id, newStatus.Status) });//, JsonRequestBehavior.AllowGet);
        }
    }
}
