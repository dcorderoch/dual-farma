using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dual_farma.DAL.Models;

namespace dual_farma.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        public User Post(User model)
        {
            return model;
        }
    }   
}
