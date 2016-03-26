using System;
using FarmaticaCore.DAL;
using FarmaticaCore.DAL.Models;
using FarmaticaCore.DAL.Repositories;

namespace FarmaticaCore.Test
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
                    MedicineId = Guid.Empty,
                    Name = "Cloretmazol",
                    RequiresPrescription = false,
                    Price = 2000,
                    OriginOffice = 0,
                    Stock = 100,
                    NumberSold = 0,
                    House="bayer"
                };
                var someId = Guid.NewGuid();

                var newMedicine2 = new Medicine()
                {
                    MedicineId =someId,
                    Name = "ritalina",
                    RequiresPrescription = false,
                    Price = 5000,
                    OriginOffice = 0,
                    Stock = 100,
                    NumberSold = 0,
                    House = "csde"
                };
                var newMedicine3 = new Medicine()
                {
                    MedicineId = otherId,
                    Name = "bentroato",
                    RequiresPrescription = false,
                    Price = 100,
                    OriginOffice = 0,
                    Stock = 100,
                    NumberSold = 0,
                    House = "neotopic"
                };

                //inserting into repository
                medicineRepo.Create(newMedicine1);
                medicineRepo.Create(newMedicine2);
                medicineRepo.Create(newMedicine3);

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
                    RequiresPrescription = false,
                    Price = 212434,
                    OriginOffice = 0,
                    Stock = 100,
                    NumberSold = 0,
                    House = "bayer"
                });
                //saving changes to the database
                uow.SaveChanges();
            }
        }
    }
}