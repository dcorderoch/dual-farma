﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dual_farma.BLL;

namespace dual_farma.Controllers
{
    public class ClientController : ApiController
    {
        // GET api/client/login
        public string[] Login([FromBody]string userID, [FromBody]string uPass)
        {
            var ACM = new Account_Manager();
            // CHANGE
            var retval = ACM.AuthorizeLogin(userID, uPass);

            if (retval[0] == Constants.SUCCESSFUL_LOGIN.ToString())
            {
                return retval;
            }
            if (retval[0] == Constants.INVALID_USER.ToString())
                return new string[] { "invalid:user", "user:invalid" };
            if (retval[0] == Constants.INVALID_PASSWORD.ToString())
            {
                return new string[] { "invalid:password", "password:invald" };
            }

            return retval;
        }

        // POST api/client/new
        public string New([FromBody]string userId, [FromBody]string password, [FromBody]string name, [FromBody]string lastName1, [FromBody]string lastName2, [FromBody]string email, [FromBody]string company, [FromBody]string roleId)
        {
            var ACM = new Account_Manager();
            // CHANGE
            if (ACM.CreateUser(userId, password, name, lastName1, lastName2, email, company, roleId) ==
                Constants.ALREADY_REGISTERED)
            {
                return "{User:alreadyInUse}";
            }
            return "{User:registered}";

        }
    }
}
