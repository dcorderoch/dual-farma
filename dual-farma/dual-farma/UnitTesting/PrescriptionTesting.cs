using System;
using System.Drawing;
using System.Runtime.InteropServices;
using dual_farma.DAL;
using dual_farma.DAL.Models;
using dual_farma.DAL.Repositories;

namespace dual_farma.UnitTesting
{
    public class PrescriptionTesting
    {
        public static void test()
        {

            var factory = new DbConnectionFactory("local");
            var context = new DbContext(factory);
            var guid=Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            //creates temporal unit of work to play with
            using (var uow = context.CreateUnitOfWork())
            {
                //initializing new user repository to commit CRUD operations
                var prescriptionRepo = new PrescriptionRepository(context);

                //Creating few new users to insert into database
                var newPrescription1 = new Prescription()
                {
                    PrescriptionID = guid,
                    Doctor = "DOC1456",
                    Image = Image.FromFile("./prescription.jpg")
                };
                var newPrescription2 = new Prescription()
                {
                    PrescriptionID= guid2,
                    Doctor = "DOC123",
                    Image = Image.FromFile("./prescription.jpg")
                };

                var newPrescription3 = new Prescription()
                {
                    PrescriptionID = Guid.NewGuid(),
                    Doctor = "DOC123",
                    Image = Image.FromFile("./prescription.jpg")
                };

                //inserting into repository
                prescriptionRepo.Create(newPrescription1);
                prescriptionRepo.Create(newPrescription2);
                prescriptionRepo.Create(newPrescription3);

                //deleting previusly created user
                prescriptionRepo.DeleteById(guid);
                //retrieving some users
                var allusers = prescriptionRepo.GetAll();
                var retrieveduser = prescriptionRepo.GetById(guid2);
                //updating an existing user
                prescriptionRepo.Update(new Prescription()
                {
                    PrescriptionID = guid2,
                    Doctor = "DOC1456",
                    Image = Image.FromFile("prescription.jpg")
                });
                //saving changes to the database
                uow.SaveChanges();
            }
        }
    }
}