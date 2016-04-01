using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dual_farma.DAL.Models;

namespace dual_farma.Models
{
    public class Data
    {
        public static List<User> theUsers;

        public Data()
        {
            theUsers = new List<User>();
            theUsers.Add(new User() { Company = "algo", Email = "algo", IdUsuario = "1", LastName1 = "lll", LastName2 = "LLL", Name = "osv", Password = "jaja", RoleId = 90 });
            theUsers.Add(new User() { Company = "algo2", Email = "algo2", IdUsuario = "2", LastName1 = "lll2", LastName2 = "LLL2", Name = "osv2", Password = "jaja2", RoleId = 902 });
        }
        // GET api/info/users
        public static List<User> GetUsers()
        {
            return theUsers;
        }
        // GET api/info/user/id
        public static User GetUser(int id)
        {
            if (id == 0)
            {
                return theUsers.ToArray()[0];
            }
            if (id == 1)
            {
                return theUsers.ToArray()[1];
            }
            return null;
        }
    }
}