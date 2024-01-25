using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMS.Core;
using SMS.Entities;
using System.Web;

namespace SMS.Web.api
{
    public class AppSettingController : EntityController<AppSetting, AppSetting, AppSetting>
    {
        protected IComboEntitySvc<AppSetting, AppSetting, AppSetting> cmbSvc;

        public AppSettingController(IComboEntitySvc<AppSetting, AppSetting, AppSetting> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }
        
        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public string GetSetting(AppSetting setting)
        {
            return (Svc as SMS.Business.Service.AppSettingSvc).GetSetting(setting.SettingKey == null ? 0 : (int)setting.SettingKey);
        }

        [HttpGet]
        public List<AppSetting> GetList()
        {
            return (Svc as SMS.Business.Service.AppSettingSvc).GetList();
        }

        [HttpPost]
        public Response Add(List<AppSetting> AppSettings)
        {
            return (Svc as SMS.Business.Service.AppSettingSvc).Add(AppSettings);
        }

        [HttpPost]
        public Response Update1(List<AppSetting> AppSettings)
        {
            return (Svc as SMS.Business.Service.AppSettingSvc).Update(AppSettings);
        }

        [HttpGet]
        public string GetAppSettingKeys()
        {
            var AppSettingKeys = typeof(SMS.Entities.AppSettingKeys)
                .GetFields()
                .ToDictionary(x => x.Name, x => x.GetValue(null));
            var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(AppSettingKeys);
            return json;
        }

        [HttpGet]
        public string GetAppSettingBoolValuedKeys()
        {
            var AppSettingBoolValuedKeys = typeof(SMS.Entities.AppSettingBoolValuedKeys)
                .GetFields()
                .ToDictionary(x => x.Name, x => x.GetValue(null));
            var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(AppSettingBoolValuedKeys);
            return json;
        }
    }
}