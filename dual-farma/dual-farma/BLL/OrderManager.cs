using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using dual_farma.DAL;
using dual_farma.DAL.Models;
using dual_farma.DAL.Repositories;

namespace dual_farma.BLL
{
    /// <summary>
    /// This class manages all the logic related to orders
    /// </summary>
    public class OrderManager
    {
        private DbConnectionFactory factory;
        private DbContext dbContext;


        public OrderManager()
        {
            this.factory = new DbConnectionFactory("local");
            this.dbContext = new DbContext(factory);
        }

        /// <summary>
        /// Creates a new order and assigns the given medicines to the created order
        /// </summary>
        /// <param name="clientId">Client id</param>
        /// <param name="medicinesId">An array with the order's medicines</param>
        /// <param name="pickupOfficeId"></param>
        /// <param name="prefPhoneNume"></param>
        /// <param name="pickUpdDate"></param>
        /// <param name="type">Type of the order</param>
        /// <returns>Success code</returns>
        public int CreateOrderWithoutPrescription(string clientId, string[] medicinesId, string pickupOfficeId,
            string prefPhoneNume, DateTime pickUpdDate, bool type)
        {
            var orderPriority = Constants.NORMAL_PRIORITY;
            using (var uow = dbContext.CreateUnitOfWork())
            {
                var orderRepo = new OrderRepository(dbContext);
                var clientRepo = new ClientRepository(dbContext);
                try
                {
                    //Gets client's number of penalties
                    var numberOfPenalties = clientRepo.GetNumberOfPenalties(clientId);
                    if (numberOfPenalties > 0)
                    {
                        orderPriority = Constants.LOW_PRIORITY;
                    }
                    Order myNewOrder = new Order()
                    {
                        OrderId = Guid.NewGuid(),
                        ClientId = clientId,
                        Type = type,
                        HasPrescription = false,
                        InvoiceImage = null,
                        PickUpOffice = Guid.Parse(pickupOfficeId),
                        PickUpdDate = pickUpdDate,
                        PrefPhoneNum = prefPhoneNume,
                        PrescriptionId = null,
                        Priority = orderPriority,
                        State = Constants.CREATED_ORDER_STATUS
                    };
                    orderRepo.Create(myNewOrder);
                    foreach (var medicineId in medicinesId)
                    {
                        orderRepo.AddMedicineinToOrder(Guid.Parse(medicineId), myNewOrder.OrderId);
                    }
                    uow.SaveChanges();
                }
                catch (Exception)
                {
                    return Constants.ERROR;
                }
            }
            return Constants.SUCCESS;
        }

        /// <summary>
        ///  Creates a new Order and a prescription, assigns the given prescripted medicines to the prescription, assigns the given medicines to the order and
        /// then assgins the created prescription to the created order.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="medicinesId"></param>
        /// <param name="prescriptedMedicinesId"></param>
        /// <param name="prescriptionImage"></param>
        /// <param name="doctorId"></param>
        /// <param name="pickupOfficeId"></param>
        /// <param name="prefPhoneNume"></param>
        /// <param name="pickUpdDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CreateOrderWithPrescription(string clientId, string[] medicinesId, string[] prescriptedMedicinesId,
            Image prescriptionImage, string doctorId, string pickupOfficeId,
            string prefPhoneNume, DateTime pickUpdDate, bool type)
        {
            var orderPriority = Constants.NORMAL_PRIORITY;
            using (var uow = dbContext.CreateUnitOfWork())
            {
                var orderRepo = new OrderRepository(dbContext);
                var clientRepo = new ClientRepository(dbContext);
                var prescriptionRepo = new PrescriptionRepository(dbContext);
                try
                {
                    //Gets client's number of penalties
                    var numberOfPenalties = clientRepo.GetNumberOfPenalties(clientId);
                    if (numberOfPenalties > 0)
                    {
                        orderPriority = Constants.LOW_PRIORITY;
                    }
                    //Creating prescription and order objects to insert into database
                    var myNewPrescription = new Prescription()
                    {
                        Doctor = doctorId,
                        Image = prescriptionImage,
                        PrescriptionID = Guid.NewGuid()
                    };
                    var myNewOrder = new Order()
                    {
                        OrderId = Guid.NewGuid(),
                        ClientId = clientId,
                        Type = type,
                        HasPrescription = true,
                        InvoiceImage = null,
                        PickUpOffice = Guid.Parse(pickupOfficeId),
                        PickUpdDate = pickUpdDate,
                        PrefPhoneNum = prefPhoneNume,
                        PrescriptionId = myNewPrescription.PrescriptionID,
                        Priority = orderPriority,
                        State = Constants.CREATED_ORDER_STATUS
                    };
                    //First, is necessary to create a new prescription into prescriptions table
                    prescriptionRepo.Create(myNewPrescription);
                    //Associating  each prescripted medicines with its order
                    foreach (var prescriptedMedicineId in prescriptedMedicinesId)
                    {
                        prescriptionRepo.AddMedicineIntoPrescription(myNewPrescription.PrescriptionID,
                            Guid.Parse(prescriptedMedicineId));
                    }
                    //Creating new order
                    orderRepo.Create(myNewOrder);
                    //Associating each medicine with its order
                    foreach (var medicineId in medicinesId)
                    {
                        orderRepo.AddMedicineinToOrder(Guid.Parse(medicineId), myNewOrder.OrderId);
                    }
                    uow.SaveChanges();
                }
                catch (Exception)
                {
                    return Constants.ERROR;
                }
            }
            return Constants.SUCCESS;
        }

        /// <summary>
        /// Returns an Order List for the given branch office id
        /// </summary>
        /// <param name="branchOfficeId"></param>
        /// <returns></returns>
        public List<Order> GetAllOrdersByBranchOffice(string branchOfficeId)
        {
            List<Order> orders;
            var orderRepo = new OrderRepository(dbContext);
            try
            {
                orders = orderRepo.GetAllOrdersByBranchOffice(Guid.Parse(branchOfficeId)) as List<Order>;
            }
            catch (Exception)
            {
                return null;
            }
            return orders;
        }

        /// <summary>
        /// Deletes existing order medicines and then deletes the order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int DeleteOrder(string orderId)
        {
            var orderRepo = new OrderRepository(dbContext);
            using (var uow = dbContext.CreateUnitOfWork())
            {
                try
                {
                    var orderToDelete = orderRepo.GetById(Guid.Parse(orderId)) as List<Order>;
                    if (orderToDelete != null)
                        foreach (var order in orderToDelete)
                        {
                            orderRepo.DeleteOrderMedicinesByOrderId(order.OrderId);
                            orderRepo.DeleteById(order.OrderId);
                        }

                }
                catch (Exception)
                {
                    return Constants.ERROR;
                }
                uow.SaveChanges();
                return Constants.SUCCESS;
            }
        }

        /// <summary>
        /// Updates an order status
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateOrderStatus(string orderId, int status)
        {
            var orderRepo = new OrderRepository(dbContext);
            try
            {
                using (var uow = dbContext.CreateUnitOfWork())
                {
                    var ordersFetched = orderRepo.GetById(Guid.Parse(orderId));
                    if (ordersFetched.Any())
                    {
                        var orderToModify = ordersFetched.First();
                        orderToModify.State = status;
                        orderRepo.Update(orderToModify);
                        uow.SaveChanges();
                    }
                    
                }
            }
            catch (Exception e)
            {
                return Constants.ERROR;
            }
            return Constants.SUCCESS;
        }



    }
}
