using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    public class LessonClassController : Controller
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

        [Authorize(Roles = "Teacher")]
        public ActionResult List_ForTeacher()
        {
            return View();
        }

    }
}