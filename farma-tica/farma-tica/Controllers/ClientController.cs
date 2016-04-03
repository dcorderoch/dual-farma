using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;
using farma_tica.BLL;
using farma_tica.DAL.Models;

namespace farma_tica.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet]
        public JsonResult Test()
        {
            return Json(new ReturnStatus() {StatusCode = 200});
        }
        
    }
}
