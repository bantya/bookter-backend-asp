﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManagment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}