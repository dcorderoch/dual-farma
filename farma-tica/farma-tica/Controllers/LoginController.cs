﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Net.Http;
using System.Security.AccessControl;
using System.Web.Http;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class LoginController : Controller
    {
        // GET api/login/login
        public string[] Login([FromBody]string userID,[FromBody]string uPass)
        {
            var ACM = new Account_Manager();
            var retval = ACM.AuthorizeLogin(userID, uPass);

            if (retval[0] == Constants.SUCCESSFUL_LOGIN.ToString())
            {
                return retval;
            }
            if (retval[0] == Constants.INVALID_USER.ToString())
                return new string[] {"invalid:user", "user:invalid"};
            if (retval[0] == Constants.INVALID_PASSWORD.ToString())
            {
                return new string[] {"invalid:password", "password:invald"};
            }

            return retval;
        }

        // POST api/login/new
        public string New([FromBody]string userId, [FromBody]string password, [FromBody]string name, [FromBody]string lastName1, [FromBody]string lastName2, [FromBody]string email, [FromBody]string company, [FromBody]string roleId)
        {
            var ACM = new Account_Manager();
            if (ACM.CreateUser(userId, password, name, lastName1, lastName2, email, company, roleId) ==
                Constants.ALREADY_REGISTERED)
            {
                return "{User:alreadyInUse}";
            }
            return "{User:registered}";
            
        }

        // GET api/login/allusers
        public List<User> AllUsers([FromBody]string company)
        {
            var ACM = new Account_Manager();
            return ACM.GetAllUsers(company);
        }

        
    }
}
