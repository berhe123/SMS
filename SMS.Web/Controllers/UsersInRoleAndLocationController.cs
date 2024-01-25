using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    public class UsersInRoleAndLocationController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult UserList()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UsersInRoleAndLocationList()
        {
            return View();
        }
    }
}
