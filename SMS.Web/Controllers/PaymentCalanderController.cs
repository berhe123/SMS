using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    public class PaymentCalanderController : Controller
    {
        [Authorize(Roles="Administrator")]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles="Administrator")]
        public ActionResult Edit(Guid id)
        {
            return View(id);
        }

        [Authorize(Roles="Administrator")]
        public ActionResult List()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ToClassRoom()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ListClassRoom()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditClassRoom(Guid id)
        {
            return View();
        }
    }
}