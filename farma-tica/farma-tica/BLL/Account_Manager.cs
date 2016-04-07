using System;
using farma_tica.DAL;
using farma_tica.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using farma_tica.DAL.Models;

namespace farma_tica.BLL
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
        /// is returned within an List of strings.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public List<string> AuthorizeLogin(string userID, string password)
        {
            List<string> retVal = new List<string>();
            using (var uow = context.CreateUnitOfWork())
            {
                var userRepo = new UserRepository(context);
                try
                {
                    var userList = (List<User>)userRepo.GetById(userID);
                    if (userList.Any())
                    {
                        var user = userList.First();
                        var opCode = user.Password == password ? Constants.SUCCESSFUL_LOGIN : Constants.INVALID_PASSWORD;
                        switch (opCode)
                        {
                            case Constants.INVALID_PASSWORD:
                                retVal.Add(Constants.INVALID_PASSWORD.ToString());
                                break;

                            case Constants.SUCCESSFUL_LOGIN:
                                retVal.Add(Constants.SUCCESSFUL_LOGIN.ToString());
                                retVal.Add(user.IdUsuario);
                                retVal.Add(user.Name);
                                retVal.Add(user.LastName1);
                                retVal.Add(user.LastName2);
                                retVal.Add(user.Email);
                                retVal.Add(user.Company);
                                retVal.Add(user.RoleId.ToString());
                                break;
                            default:
                                retVal.Add(Constants.INVALID_USER.ToString());
                                break;
                        }
                    }
                    else
                    {
                        retVal.Add(Constants.INVALID_USER.ToString());
                    }
                }
                catch (Exception)
                {
                    retVal.Add(Constants.INVALID_USER.ToString());
                    return retVal;
                }
            }
            return retVal;
        }
    }
}