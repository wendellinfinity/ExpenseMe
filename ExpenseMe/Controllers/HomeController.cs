﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseMe.Controllers {

    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Message = "Rawr!";

            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}
