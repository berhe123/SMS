using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    public class SubjectTeacherClassRoomController : Controller
    {
        [Authorize(Roles = "TeacherAdmin")]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = "TeacherAdmin")]
        public ActionResult Edit(Guid id)
        {
            return View(id);
        }

        [Authorize(Roles = "TeacherAdmin")]
        public ActionResult List()
        {
            return View();
        }
    }
}