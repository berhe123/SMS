using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SMS.Web.Controllers
{
    public class GroupMemberController : Controller
    {       
        [Authorize(Roles = "Teacher")]
        public ActionResult StudentAssign()
        {
            return View();
        }
    }
}