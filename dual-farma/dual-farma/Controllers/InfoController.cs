using dual_farma.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dual_farma.Models;

namespace dual_farma.Controllers
{
    public class InfoController : ApiController
    {
        public string Get()
        {
            return "ok";
        }
        public static List<User> GetUsers()
        {
            return Data.GetUsers();
        }
        // GET api/info/user/id
        public static User GetUser(int id)
        {
            return Data.GetUser(id);
        }
    }
}
