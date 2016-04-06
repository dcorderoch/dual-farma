﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace farma_tica.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Test()
        {
            return Json(new ReturnStatus() {StatusCode = 200}, JsonRequestBehavior.AllowGet);
        }
    }
}