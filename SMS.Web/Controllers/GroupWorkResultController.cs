﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    public class GroupWorkResultController : Controller
    {
        [Authorize(Roles = "Teacher")]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(Guid id)
        {
            return View(id);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult List()
        {
            return View();
        }
    }
}