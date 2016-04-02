using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
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
        DbConnectionFactory factory;
        DbContext dbContext;


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
        public int CreateOrderWithoutPrescription(string clientId, string[] medicinesId, int pickupOfficeId,
            string prefPhoneNume, DateTime pickUpdDate, bool type)
        {
            var orderPriority = "Normal";
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
                        orderPriority = "Baja";
                    }
                    Order myNewOrder = new Order()
                    {
                        OrderId = Guid.NewGuid(),
                        ClientId = clientId,
                        Type = type,
                        HasPrescription = false,
                        InvoiceImage = null,
                        PickUpOffice = pickupOfficeId,
                        PickUpdDate = pickUpdDate,
                        PrefPhoneNum = prefPhoneNume,
                        PrescriptionId = null,
                        Priority = orderPriority,
                        State = 0
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
            Image prescriptionImage, string doctorId, int pickupOfficeId,
            string prefPhoneNume, DateTime pickUpdDate, bool type)
        {
            var orderPriority = "Normal";
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
                        orderPriority = "Baja";
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
                        PickUpOffice = pickupOfficeId,
                        PickUpdDate = pickUpdDate,
                        PrefPhoneNum = prefPhoneNume,
                        PrescriptionId = myNewPrescription.PrescriptionID,
                        Priority = orderPriority,
                        State = 0
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
        public List<Order> GetAllOrdersByBranchOffice(int branchOfficeId)
        {
            List<Order> orders;
            var orderRepo = new OrderRepository(dbContext);
            try
            {
                orders = orderRepo.GetAllOrdersByBranchOffice(branchOfficeId) as List<Order>;
            }
            catch (Exception)
            {
                return null;
            }
            return orders;
        }
    }
}
