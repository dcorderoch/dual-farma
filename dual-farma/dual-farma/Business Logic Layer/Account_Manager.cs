using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dual_farma.Business_Logic_Layer
{
    public class Account_Manager
    {
        private string userID;
        private string password;

        public Account_Manager(string userID, string password)
        {
            userID = this.userID;
            password = this.password;
        }
    }
}