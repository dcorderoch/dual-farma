using System;
using dual_farma.DAL;
using dual_farma.DAL.Models;
using dual_farma.DAL.Repositories;

namespace dual_farma.UnitTesting
{
    public class MedicineTesting
    {
        public static void test()
        {

            var factory = new DbConnectionFactory("local");
            var context = new DbContext(factory);

            //creates temporal unit of work to play with
            using (var uow = context.CreateUnitOfWork())
            {
                //initializing new user repository to commit CRUD operations
                var medicineRepo  = new MedicineRepository(context);

                var otherId = Guid.NewGuid();
                //Creating few new users to insert into database
                var newMedicine1 = new Medicine()
                {
                    MedicineId = Guid.NewGuid(),
                    Name = "Cloretmazol",
                    RequiresPrescription = false
                };
                var someId = Guid.NewGuid();

                var newMedicine2 = new Medicine()
                {
                    MedicineId =someId,
                    Name = "ritalina",
                    RequiresPrescription = false
                };
                var newMedicine3 = new Medicine()
                {
                    MedicineId = otherId,
                    Name = "bentroato",
                    RequiresPrescription = false
                };

                var newMedicine4 = new Medicine()
                {
                    MedicineId = Guid.NewGuid(),
                    Name = "Clorotrimet",
                    RequiresPrescription = false
                };

                //inserting into repository
                medicineRepo.Create(newMedicine1);
                medicineRepo.Create(newMedicine2);
                medicineRepo.Create(newMedicine3);
                medicineRepo.Create(newMedicine4);

                //deleting previusly created user
                medicineRepo.DeleteById(otherId);
                //retrieving some users
                var allusers = medicineRepo.GetAll();
                var retrieveduser = medicineRepo.GetById(someId);
                //updating an existing user
                medicineRepo.Update(new Medicine()
                {
                    MedicineId = someId,
                    Name = "Cloretmazol",
                    RequiresPrescription = false
                });
                
                //saving changes to the database
                uow.SaveChanges();
                var i = medicineRepo.GetAmmountSoldByCompany("Farmatica");
                var j = medicineRepo.GetAmmountSoldByCompany("Phishel");
                var k = medicineRepo.GetTotalMostSold();
                var h = medicineRepo.GetTotalMostSoldByCompany("Farmatica");
                var l = medicineRepo.GetTotalMostSoldByCompany("Phishel");
            }
        }
    }
}