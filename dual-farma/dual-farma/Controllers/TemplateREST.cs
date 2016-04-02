using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dual_farma.DAL.Models;

namespace dual_farma.Controllers
{
    public class TemplateREST
    {
        public static List<User> _theUsers = new List<User>();
        //static _theUsers = new List<User>();

        public TemplateREST()
        {
            User newUser = new User
            {
                IdUsuario = "dan",
                Password = "123",
                Name = "daniel",
                LastName1 = "moraga",
                LastName2 = "mora",
                Email = "dan@nana.com",
                RoleId = Convert.ToInt32(2)
            };
            //newUser.Company = company;

            User newUser2 = new User
            {
                IdUsuario = "dana",
                Password = "123a",
                Name = "daniela",
                LastName1 = "moragaaa",
                LastName2 = "moraaa",
                Email = "dan@nana.comaa",
                RoleId = Convert.ToInt32(1)
            };
            //newUser2.Company = company;

            _theUsers.Add(newUser);
            _theUsers.Add(newUser2);
        }

        public static int InsertUser(string id, string pass, string name, string lname, string lname2, string email, string rol)
        {
            User newUser = new User()
            {
                IdUsuario = id,
                Password = pass,
                Name = name,
                LastName1 = lname,
                LastName2 = lname2,
                Email = email,
                RoleId = Convert.ToInt32(rol)
            };
            _theUsers.Add(newUser);
            return 1;
        }
        public static List<User> GetAllUsers()
        {
            return _theUsers;
        }
    }
}