using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    public class AppSettingController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult List()
        {
            return View();
        }

        //[Authorize(Roles = "Administrator")]
        //public ActionResult GetAppSettingKeys()
        //{
        //    var AppSettingKeys = typeof(SMS.Entities.AppSettingKeys)
        //        .GetFields()
        //        .ToDictionary(x => x.Name, x => x.GetValue(null));
        //    var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(AppSettingKeys);
        //    return JavaScript("var AppSettingKeys = " + json + ";");
        //}
    }
}
