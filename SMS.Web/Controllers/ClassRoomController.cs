using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SMS.Web.Controllers
{
    public class ClassRoomController : Controller
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
    }
}