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
        // URI from Angular: api/Pedido/Create
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
                });
        }

        // URI from Angular: api/Pedido/CreateWPrescription
        [HttpPost]
        public JsonResult<ReturnStatus> CreateWPrescription(OrderWithPrescription theOrder)
        {
            var om = new OrderManager();
            return Json(new ReturnStatus()
            {
                StatusCode = om.CreateOrderWithPrescription(
                    theOrder.clientId,theOrder.medicinesId,theOrder.prescriptedMedicinesId,theOrder.prescriptionImage,theOrder.DoctorId,theOrder.pickupOfficeId,theOrder.prefPhoneNumber,theOrder.pickupDate,theOrder.type)
            });
        }

        // URI from Angular: api/Pedido/GetAllByBranchOffice
        [HttpPost]
        public JsonResult<List<Order>> GetAllByBranchOffice(IdBranchOffice branchOfficeId)
        {
            var om = new OrderManager();
            return Json(om.GetAllOrdersByBranchOffice(branchOfficeId.boID));
        }

        // URI from Angular: api/Pedido/Delete
        [HttpPost]
        public JsonResult<ReturnStatus> Delete(IdOrder orderId)
        {
            var om = new OrderManager();
            return Json(new ReturnStatus() {StatusCode = om.DeleteOrder(orderId.oID)});
        }

        // URI from Angular: api/Pedido/Update
        [HttpPost]
        public JsonResult<ReturnStatus> Update(OrderStatus newStatus)
        {
            var om = new OrderManager();
            return Json(new ReturnStatus() { StatusCode = om.UpdateOrderStatus(newStatus.Id, newStatus.Status) });
        }
    }
}
