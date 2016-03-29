using System;
using FarmaticaCore.DAL;
using FarmaticaCore.DAL.Models;
using FarmaticaCore.DAL.Repositories;

namespace FarmaticaCore.Test
{
    public class ClientTesting
    {
        public static void test()
        {

            var factory = new DbConnectionFactory("local");
            var context = new DbContext(factory);

            //creates temporal unit of work to play with
            using (var uow = context.CreateUnitOfWork())
            {
                //initializing new user repository to commit CRUD operations
                var clientRepo = new ClientRepository(context);

                //Creating few new users to insert into database
                var newClient1 = new Client()
                {
                    Id = "115240456",
                    BornDate = DateTime.Today,
                    Name = "Cliente1",
                    LastName1 = "Apellido1",
                    LastName2 = "Apellido2",
                    PhoneMum = "22388484",
                    MedicalHistory = "Alergias, nariz taponeada",
                    PlaceResidence = "Bel'en",
                    PenaltiesNumber = 0,
                    };
                var newClient2 = new Client()
                {
                    Id = "114679506",
                    BornDate = DateTime.Today,
                    Name = "Cliente2",
                    LastName1 = "Apellido1",
                    LastName2 = "Apellido2",
                    PhoneMum = "45434545",
                    MedicalHistory = "barbituricos",
                    PlaceResidence = "Heredia",
                    PenaltiesNumber = 5
                }; var newClient3 = new Client()
                {
                    Id = "788985536",
                    BornDate = DateTime.Today,
                    Name = "Cliente3",
                    LastName1 = "Apellido1",
                    LastName2 = "Apellido2",
                    PhoneMum = "44738984",
                    MedicalHistory = "Dolores,Nauseas, nariz taponeada",
                    PlaceResidence = "Cacago",
                    PenaltiesNumber = 10
                }; var newClient4 = new Client()
                {
                    Id = "23849504",
                    BornDate = DateTime.Today,
                    Name = "Client23",
                    LastName1 = "asasa",
                    LastName2 = "dfdfdf",
                    PhoneMum = "33333333",
                    MedicalHistory = "Algunas molestias espalda",
                    PlaceResidence = "Alajuela",
                    PenaltiesNumber = 0
                };

                //inserting into repository
                clientRepo.Create(newClient1);
                clientRepo.Create(newClient2);
                clientRepo.Create(newClient3);
                clientRepo.Create(newClient4);

                //deleting previusly created repo
                clientRepo.DeleteById("788985536");
                //retrieving some users
                var allusers = clientRepo.GetAll();
                var retrieveduser = clientRepo.GetById("115240456");
                //updating an existing user
                clientRepo.Update(new Client()
                {
                    Id = "23849504",
                    BornDate = DateTime.Today,
                    Name = "nombreCambiado",
                    LastName1 = "asasa",
                    LastName2 = "dfdfdf",
                    PhoneMum = "44444444",
                    MedicalHistory = "Algunas molestias espalda",
                    PlaceResidence = "Alajuela",
                    PenaltiesNumber = 5
                });
                //saving changes to the database
                uow.SaveChanges();
            }
        }
    }
}