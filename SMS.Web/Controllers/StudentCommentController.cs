using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    public class StudentCommentController : Controller
    {
        [Authorize(Roles="Student")]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public ActionResult Edit(Guid id)
        {
            return View(id);
        }

        [Authorize(Roles = "Student")]
        public ActionResult List()
        {
            return View();
        }
    }
}