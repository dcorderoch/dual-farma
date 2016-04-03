using System;
using dual_farma.DAL;
using dual_farma.DAL.Repositories;
using System.Collections.Generic;
using dual_farma.DAL.Models;

namespace dual_farma.BLL
{
    /// <summary>
    /// Account_Manager is intended to validate most of the business rules related to the Users' login. 
    /// </summary>
    public class Account_Manager
    {
        /// <summary>
        /// Class members which allow connecting to the Database.
        /// </summary>
        private DbConnectionFactory factory;
        private DbContext context;

        public Account_Manager()
        {
            factory = new DbConnectionFactory("Azure");
            context = new DbContext(factory);
        }

        /// <summary>
        /// Method that communicates directly to the REST API, providing it with the User's details in case the login results succesful. Otherwise an error code
        /// is returned within an Array List of strings.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string[] AuthorizeLogin(string userID, string password)
        {
            string[] response = { };
            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                var userList = (List<User>)userRepo.GetById(userID);
                var user = userList[0];
                var opCode = VerifyUser(userID, password);
                switch (opCode)
                {
                    case Constants.INVALID_USER:
                        response[0] = Constants.INVALID_USER.ToString();
                        return response;

                    case Constants.INVALID_PASSWORD:
                        response[0] = Constants.INVALID_PASSWORD.ToString();
                        return response;

                    case Constants.SUCCESSFUL_LOGIN:
                        response[0] = Constants.SUCCESSFUL_LOGIN.ToString();
                        response[1] = user.IdUsuario;
                        response[2] = user.Name;
                        response[3] = user.LastName1;
                        response[4] = user.LastName2;
                        response[5] = user.Email;
                        //response[6] = user.Company;
                        response[7] = user.RoleId.ToString();
                        return response;
                    default:
                        response[0] = Constants.INVALID_USER.ToString();
                        return response;
                }
            }
        }

        /// <summary>
        /// Function that creates a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> Integer indicating whether the creation was successful.</returns>
        public int CreateUser(string userId, string password, string name, string lastName1, string lastName2, string email, string company, string roleId)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                User newUser = new User();
                try
                {
                    newUser.IdUsuario = userId;
                    newUser.Password = password;
                    newUser.Name = name;
                    newUser.LastName1 = lastName1;
                    newUser.LastName2 = lastName2;
                    newUser.Email = email;
                    //newUser.Company = company;
                    newUser.RoleId = Convert.ToInt32(roleId);
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
                    userList = (List<User>) userRepo.GetAll();
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
        public int UpdateUser(string userId, string password, string name, string lastName1, string lastName2, string email, string company, string roleId)
        {
            var response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                User newUser = new User();
                try
                {
                    newUser.IdUsuario = userId;
                    newUser.Password = password;
                    newUser.Name = name;
                    newUser.LastName1 = lastName1;
                    newUser.LastName2 = lastName2;
                    newUser.Email = email;
                    //newUser.Company = company;
                    newUser.RoleId = Convert.ToInt32(roleId);
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

        /// <summary>
        /// Auxiliary function that finds out the existence of a User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Boolean indicating the existence of the given user.</returns>
        private bool DoesUserExist(string userId)
        {
            bool exists;
            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                var userList = (List<User>)userRepo.GetById(userId);
                exists = userList.Count != 0;
            }
            return exists;
        }

        /// <summary>
        /// Verifies the existence of a User given its ID and Password
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns>Operation code that indicates whether it exists in the database or not, or the password given is incorrect.</returns>
        private int VerifyUser(string userID, string password)
        {
            var result = 0;
            using (var uow = context.CreateUnitOfWork())
            {

                var doesExist = DoesUserExist(userID);
                if (!doesExist)
                {
                    result = Constants.INVALID_USER;
                }
                else
                {
                    var userRepo = new UserRepository(context);
                    var userList = (List<User>)userRepo.GetById(userID);
                    result = userList[0].Password != password ? Constants.INVALID_PASSWORD : Constants.SUCCESSFUL_LOGIN;
                }
            }
            return result;
        }
    }
}