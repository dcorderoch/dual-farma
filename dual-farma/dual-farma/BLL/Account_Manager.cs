using System;
using FarmaticaCore.DAL;
using FarmaticaCore.DAL.Repositories;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using FarmaticaCore.DAL.Models;

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
            string[] response = {};
            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                var userList = (List<User>) userRepo.GetById(userID);
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
                        response[6] = user.RoleId.ToString();
                        return response;
                    default:
                        response[0] = Constants.INVALID_USER.ToString();
                        return response;
                }
            }
        }

        public int CreateUser(string[] user)
        {
            int response = 0;
            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                var userList = (List<User>)userRepo.GetAll();
                bool doesUserExist;
                string userId = user[0];





            }
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
                var userRepo = new UserRepository(context);
                var userList = (List<User>) userRepo.GetById(userID);

                if (userList.Count == 0)
                {
                    result = Constants.INVALID_USER;
                }
                else
                {
                    result = userList[0].Password != password ? Constants.INVALID_PASSWORD : Constants.SUCCESSFUL_LOGIN;
                }
            }
            return result;
        }
    
      }
}