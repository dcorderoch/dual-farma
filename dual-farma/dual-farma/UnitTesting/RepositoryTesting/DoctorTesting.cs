using dual_farma.DAL;
using dual_farma.DAL.Models;
using dual_farma.DAL.Repositories;

namespace dual_farma.UnitTesting
{
    public class DoctorTesting
    {
        public static void test()
        {

            var factory = new DbConnectionFactory("local");
            var context = new DbContext(factory);

            //creates temporal unit of work to play with
            using (var uow = context.CreateUnitOfWork())
            {
                //initializing new user repository to commit CRUD operations
                var doctorRepo = new DoctorRepository(context);

                //Creating few new users to insert into database
                var newDoctor1 = new Doctor()
                {
                    DoctorId = "DOC123",
                    Name = "Miguel",
                    PlaceResidence = "Heredia",
                    IdNumber = "113658874",
                    LastName1 = "Gonzalez",
                    LastName2 = "Fallas"
                };
                var newDoctor2 = new Doctor()
                {
                    DoctorId = "DOC1456",
                    Name = "Julio",
                    PlaceResidence = "Cartago",
                    IdNumber = "345659873",
                    LastName1 = "Machado",
                    LastName2 = "Falcao"
                };
                var newDoctor3 = new Doctor()
                {
                    DoctorId = "DOC345",
                    Name = "Pedro",
                    PlaceResidence = "Alajuela",
                    IdNumber = "324548876",
                    LastName1 = "Pica",
                    LastName2 = "Piedra"
                };
                var newDoctor4 = new Doctor()
                {
                    DoctorId = "DOC456",
                    Name = "Mauricio",
                    PlaceResidence = "Limon",
                    IdNumber = "888877663",
                    LastName1 = "Espinoza",
                    LastName2 = "Gonzales"
                };

                //inserting into repository
                doctorRepo.Create(newDoctor1);
                doctorRepo.Create(newDoctor2);
                doctorRepo.Create(newDoctor3);
                doctorRepo.Create(newDoctor4);

                //deleting previusly created user
                doctorRepo.DeleteById("DOC345");
                //retrieving some users
                var allusers = doctorRepo.GetAll();
                var retrieveduser = doctorRepo.GetById("DOC456");
                //updating an existing user
                doctorRepo.Update(new Doctor()
                {
                    DoctorId = "DOC1456",
                    Name = "JulioEdited",
                    PlaceResidence = "Cartago",
                    IdNumber = "345659873",
                    LastName1 = "Machado",
                    LastName2 = "Falcao"
                });
                //saving changes to the database
                uow.SaveChanges();
            }
        }
    }
}