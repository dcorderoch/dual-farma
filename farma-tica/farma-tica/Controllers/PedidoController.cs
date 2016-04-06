using System.Web.Mvc;
using farma_tica.BLL;

namespace farma_tica.Controllers
{
    public class PedidoController : Controller
    {
        // URI from Angular: /Pedido/Create
        [HttpPost]
        public JsonResult Create(OrderWithoutPrescription theOrder)
        {
            var om = new OrderManager();
            return
                Json(new ReturnStatus()
                {
                    StatusCode =
                        om.CreateOrderWithoutPrescription(theOrder.clientId, theOrder.medicineIds,
                            theOrder.pickupOfficeId, theOrder.prefPhoneNumber, theOrder.pickupDate, theOrder.type)
                },JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Pedido/CreateWPrescription
        [HttpPost]
        public JsonResult CreateWPrescription(OrderWithPrescription theOrder)
        {
            var om = new OrderManager();
            return Json(new ReturnStatus()
            {
                StatusCode = om.CreateOrderWithPrescription(
                    theOrder.clientId,theOrder.medicinesId,theOrder.prescriptedMedicinesId,theOrder.prescriptionImage,theOrder.DoctorId,theOrder.pickupOfficeId,theOrder.prefPhoneNumber,theOrder.pickupDate,theOrder.type)
            }, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Pedido/GetAllByBranchOffice
        [HttpPost]
        public JsonResult GetAllByBranchOffice(string branchOfficeId)
        {
            var om = new OrderManager();
            return Json(om.GetAllOrdersByBranchOffice(branchOfficeId),JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Pedido/Delete
        [HttpPost]
        public JsonResult Delete(string orderId)
        {
            var om = new OrderManager();
            return Json(new ReturnStatus() {StatusCode = om.DeleteOrder(orderId)}, JsonRequestBehavior.AllowGet);
        }

        // URI from Angular: /Pedido/Update
        [HttpPost]
        public JsonResult Update(OrderStatus newStatus)
        {
            var om = new OrderManager();
            return Json(new ReturnStatus() { StatusCode = om.UpdateOrderStatus(newStatus.Id, newStatus.Status) }, JsonRequestBehavior.AllowGet);
        }
    }
}
