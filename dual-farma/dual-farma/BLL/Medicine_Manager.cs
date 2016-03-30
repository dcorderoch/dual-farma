using System;
using System.Collections.Generic;
using dual_farma.DAL;
using dual_farma.DAL.Models;
using dual_farma.DAL.Repositories;

namespace dual_farma.BLL
{
    public class Medicine_Manager
    {
        /// <summary>
        /// Class members which allow connecting to the Database.
        /// </summary>
        private DbConnectionFactory factory;
        private DbContext context;

        public Medicine_Manager()
        {
            factory = new DbConnectionFactory("Azure");
            context = new DbContext(factory);
        }
        /// <summary>
        /// Function that creates a new medicine.
        /// </summary>
        /// <param name="medicine"></param>
        /// <returns> Integer indicating whether the creation was successful.</returns>
        public int CreateMedicine(string[] medicine)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var medicineRepo = new MedicineRepository(context);
                Medicine newMedicine = new Medicine();
                try
                {
                    newMedicine.MedicineId = ;
                    newUser.Password = user[1];
                    newUser.Name = user[2];
                    newUser.LastName1 = user[3];
                    newUser.LastName2 = user[4];
                    newUser.Email = user[5];
                    //newUser.Company = user[6];
                    newUser.RoleId = Convert.ToInt32(user[7]);
                    userRepo.Create(newUser);
                    uow.SaveChanges();
                    response = Constants.USER_CREATED;

                }
                catch (Exception)
                {
                    response = Constants.ALREADY_REGISTERED;
                }
            }
            return response;
        }

        /// <summary>
        /// Obtains all users from a specific company.
        /// </summary>
        /// <param name="company"></param>
        /// <returns>List<User> that contains all the users of the specified column</returns>
        public List<User> GetAllUsers(string company)
        {
            List<User> userList = new List<User>();
            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                try
                {
                    //userList = (List<User>) userRepo.GetAll(company);
                }
                catch (Exception)
                {
                    userList = null;
                }
            }
            return userList;
        }

        /// <summary>
        /// Updates given user if possible.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Integer with the result of the update.</returns>
        public int UpdateUser(string[] user)
        {
            var response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                User newUser = new User();
                try
                {
                    newUser.IdUsuario = user[0];
                    newUser.Password = user[1];
                    newUser.Name = user[2];
                    newUser.LastName1 = user[3];
                    newUser.LastName2 = user[4];
                    newUser.Email = user[5];
                    //newUser.Company = user[6];
                    newUser.RoleId = Convert.ToInt32(user[7]);
                    userRepo.Update(newUser);
                    uow.SaveChanges();
                    response = Constants.USER_UPDATED;
                }
                catch (Exception)
                {
                    response = Constants.USER_NOT_UPDATED;
                }
            }
            return response;
        }
        /// <summary>
        /// Deletes the given user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Integer indicating whether the deletion completed successfully.</returns>
        public int DeleteUser(string userId)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                try
                {
                    userRepo.DeleteById(userId);
                    uow.SaveChanges();
                    response = Constants.USER_DELETED;
                }
                catch (Exception)
                {
                    response = Constants.USER_NOT_DELETED;
                }

            }
            return response;
        }


    }
}