using System;
using dual_farma.DAL;
using dual_farma.DAL.Models;
using dual_farma.DAL.Repositories;

namespace dual_farma.UnitTesting
{
    public class OrderTesting
    {
        public static void test()
        {

            var factory = new DbConnectionFactory("local");
            var context = new DbContext(factory);

            //creates temporal unit of work to play with
            using (var uow = context.CreateUnitOfWork())
            {
                //initializing new user repository to commit CRUD operations
                var orderRepo = new OrderRepository(context);

                var otherId = Guid.NewGuid();
                //Creating few new users to insert into database
                var newOrder1 = new Order()
                {
                   OrderId = otherId,
                   ClientId = "115240456",
                   PrescriptionId = null,
                   PickUpOffice = 1,
                   InvoiceImage = null,
                   HasPrescription = false,
                   State = 1,
                   Priority = "High",
                   PrefPhoneNum = "22355445",
                   PickUpdDate = DateTime.Now
                };
                var someId = Guid.NewGuid();
                
                var newOrder2 = new Order()
                {
                    OrderId = someId,
                    ClientId = "114679506",
                    PrescriptionId = null,
                    PickUpOffice = 1,
                    InvoiceImage = null,
                    HasPrescription = false,
                    State = 1,
                    Priority = "High",
                    PrefPhoneNum = "55443322",
                    PickUpdDate = DateTime.Now
                };
                var newOrder3 = new Order()
                {
                    OrderId = Guid.NewGuid(),
                    PrescriptionId = null,
                    ClientId = "114679506",
                    PickUpOffice = 1,
                    InvoiceImage = null,
                    HasPrescription = false,
                    State = 1,
                    Priority = "High",
                    PrefPhoneNum = "99887766",
                    PickUpdDate = DateTime.Now
                };

                //inserting into repository
                orderRepo.Create(newOrder1);
                orderRepo.Create(newOrder2);
                orderRepo.Create(newOrder3);

                //deleting previusly created user
                orderRepo.DeleteById(otherId);
                //retrieving some users
                var allusers = orderRepo.GetAll();
                var retrieveduser = orderRepo.GetById(someId);
                //updating an existing user
                orderRepo.Update(new Order()
                {
                    OrderId = someId,
                    ClientId = "23849504",
                    PrescriptionId = null,
                    PickUpOffice = 1,
                    InvoiceImage = null,
                    HasPrescription = false,
                    State = 1,
                    Priority = "High",
                    PrefPhoneNum = "11223344",
                    PickUpdDate = DateTime.Now
                });
                //saving changes to the database
                uow.SaveChanges();
            }
        }
    }
}