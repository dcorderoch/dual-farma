using FarmaticaCore.DAL;
using FarmaticaCore.DAL.Models;
using FarmaticaCore.DAL.Repositories;

namespace FarmaticaCore.Test
{
    public static class RoleTesting
    {
        public static void test()
        {

            var factory = new DbConnectionFactory("local");
            var context = new DbContext(factory);

            //creates temporal unit of work to play with
            using (var uow = context.CreateUnitOfWork())
            {
                //initializing new role repository to commit CRUD operations
                var roleRepo = new RoleRepository(context);

                //Creating few new roles to insert into database
                var newRole1 = new Role()
                {
                    Id = 1,
                    RoleName = "Administrador"
                };
                var newRole2 = new Role()
                {
                    Id = 2,
                    RoleName = "Dependiente"
                };
                var newRole3 = new Role()
                {
                    Id = 3,
                    RoleName = "AlgunOtro"
                };

                //inserting into repository
                roleRepo.Create(newRole1);
                roleRepo.Create(newRole2);

                //deleting previusly created user
                roleRepo.DeleteById(3);
                //retrieving some users
                var allroles = roleRepo.GetAll();
                var retrieveduser = roleRepo.GetById(2);
                //updating an existing user
                roleRepo.Update(new Role()
                {
                    Id = 1,
                    RoleName = "Administrador1"
                });
                //saving changes to the database
                uow.SaveChanges();
            }
        }
    }
}