﻿using dual_farma.DAL;
using dual_farma.DAL.Models;
using dual_farma.DAL.Repositories;

namespace dual_farma.UnitTesting
{
    static class UserTesting
    {
        public static void test()
        {

            var factory = new DbConnectionFactory("local");
            var context = new DbContext(factory);

            //creates temporal unit of work to play with
            using (var uow = context.CreateUnitOfWork())
            {
                //initializing new user repository to commit CRUD operations
                var userRepo = new UserRepository(context);

                //Creating few new users to insert into database
                var newUser1 = new User()
                {
                    Name = "Manuel",
                    IdUsuario = "manzumbado",
                    Password = "cacabubu",
                    LastName1 = "Zumbado",
                    LastName2 = "Corrales",
                    Email = "manzumbado@itcr.ac.cr",
                    RoleId = 1,
                    Company = "Farmatica"
                };
                var newUser2 = new User()
                {
                    Name = "Kevin",
                    IdUsuario = "kevuo",
                    Password = "charlatan1",
                    LastName1 = "Umana",
                    LastName2 = "Ortega",
                    Email = "kevuo@itcr.ac.cr",
                    RoleId = 1,
                    Company = "Phishel"
                };
                var newUser3 = new User()
                {
                    Name = "El David",
                    IdUsuario = "david",
                    Password = "mocosoft",
                    LastName1 = "Guerrero",
                    LastName2 = "Morales",
                    Email = "eldavid@itcr.ac.cr",
                    RoleId = 2,
                    Company ="Farmatica"
                };
                var newUser4 = new User()
                {
                    Name = "Nicolas",
                    IdUsuario = "majesco",
                    Password = "nico123",
                    LastName1 = "Perez",
                    LastName2 = "Gonzalez",
                    Email = "majesco@itcr.ac.cr",
                    RoleId = 2,
                    Company = "Farmatica"
                };

                //inserting into repository
                userRepo.Create(newUser1);
                userRepo.Create(newUser2);
                userRepo.Create(newUser3);
                userRepo.Create(newUser4);

                //deleting previusly created user
                userRepo.DeleteById("david");
                //retrieving some users
                var allusers = userRepo.GetAll();
                var retrieveduser = userRepo.GetById("majesco");
                //updating an existing user
                userRepo.Update(new User()
                {
                    Name = "Manueleditado",
                    IdUsuario = "manzumbado",
                    Password = "aa",
                    LastName1 = "Zumbado",
                    LastName2 = "Corrales",
                    Email = "manzumbado@itcr.ac.cr",
                    RoleId = 2,Company = "Farmatica"
                });
                //saving changes to the database
                uow.SaveChanges();
            }
        }
    }
}