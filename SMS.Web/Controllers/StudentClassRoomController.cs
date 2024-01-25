using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    public class StudentClassRoomController : Controller
    {
        [Authorize(Roles="Finance")]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = "Finance")]
        public ActionResult Edit(Guid id)
        {
            return View(id);
        }

        [Authorize(Roles = "Finance")]
        public ActionResult List()
        {
            return View();
        }
    }
}