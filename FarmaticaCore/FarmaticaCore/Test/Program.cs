using FarmaticaCore.DAL;
using FarmaticaCore.DAL.Models;
using FarmaticaCore.DAL.Repositories;

namespace FarmaticaCore.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new DbConnectionFactory("local");
            var context = new DbContext(factory);

            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                var newUser1 = new User()
                {
                    Name = "Manuel",
                    IdUsuario = "manzumbado",
                    Password = "cacabubu",
                    LastName1 = "Zumbado",
                    LastName2 = "Corrales",
                    Email = "manzumbado@itcr.ac.cr",
                    Role = 1
                };
                var newUser2 = new User()
                {
                    Name = "Kevin",
                    IdUsuario = "kevuo",
                    Password = "charlatan1",
                    LastName1 = "Umana",
                    LastName2 = "Ortega",
                    Email = "kevuo@itcr.ac.cr",
                    Role = 1
                };
                var newUser3 = new User()
                {
                    Name = "El David",
                    IdUsuario = "david",
                    Password = "mocosoft",
                    LastName1 = "Guerrero",
                    LastName2 = "Morales",
                    Email = "eldavid@itcr.ac.cr",
                    Role = 2
                };
                var newUser4 = new User()
                {
                    Name = "Nicolas",
                    IdUsuario = "majesco",
                    Password = "nico123",
                    LastName1 = "Perez",
                    LastName2 = "Gonzalez",
                    Email = "majesco@itcr.ac.cr",
                    Role = 2
                };

                userRepo.Create(newUser1);
                userRepo.Create(newUser2);
                userRepo.Create(newUser3);
                userRepo.Create(newUser4);

                uow.SaveChanges();
            }
        }
    }
}
