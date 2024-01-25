using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class AppSettingSvc : EntityBaseSvc<AppSetting>, IComboEntitySvc<AppSetting, AppSetting, AppSetting>
    {

        public AppSettingSvc(ILogger logger)
            : base(logger, "Application Setting")
        {
       
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.AppSettings
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.SettingValue
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.AppSettings
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.SettingValue
                    };

            return q.AsEnumerable();
        }

        public AppSetting GetServiceFilters()
        {
            AppSetting AppSetting = new AppSetting();
            return new AppSetting();
        }

        public IQueryable<AppSetting> Get()
        {
            return Db.AppSettings;
        }

        public AppSetting GetModel(Guid? userId)
        {
            return new AppSetting();
           
        }

        public AppSetting GetModel(Guid id)
        {
            var AppSetting = this.Get(id);
            return AppSetting;
        }

        public AppSetting GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel(id) : this.GetModel(userId);
        }

        public AppSetting Get(Guid id)
        {
            return Db.AppSettings.SingleOrDefault(e => e.TableRowGuid == id);
        }
        public string GetSetting(int SettingKey)
        {
            return Db.AppSettings.SingleOrDefault(e => e.SettingKey == SettingKey).SettingValue;
        }
                        
        public JsTable<AppSetting> Get(PageDetail pageDetail)
        {
            var query = Db.AppSettings.AsQueryable();

            List<Expression<Func<AppSetting, bool>>> filters = new List<Expression<Func<AppSetting, bool>>>();

            //if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.Name.Contains(pageDetail.Search));


            Func<AppSetting, AppSetting> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public List<AppSetting> GetList()
        {
            var query = Db.AppSettings.AsQueryable();

            Func<AppSetting, AppSetting> result = e => e;

            return query.OrderBy(o => o.SettingKey).ToList();
        }

        //public AppSettingKeys GetAppSettingKeys()
        //{
        //    AppSettingKeys AppSettingKeys = new AppSettingKeys();
        //    return AppSettingKeys;
        //}

        public Response Add(AppSetting AppSetting, Guid? userid)
        {

            AppSetting.TableRowGuid = Guid.NewGuid();
            AppSetting.UserId = userid;

            return base.Add(AppSetting);
        }

        public Response Add(List<AppSetting> AppSettings, Guid? userid)
        {

            foreach (AppSetting AppSetting in AppSettings)
            {
                AppSetting.TableRowGuid = Guid.NewGuid();
                AppSetting.UserId = userid;
            }

            return base.Add(AppSettings);
        }

        public Response Update(AppSetting AppSetting, Guid? userid)
        {
            AppSetting.UserId = userid;
            return base.Update(AppSetting);
        }

        public Response Update(List<AppSetting> AppSettings, Guid? userid)
        {
            foreach (AppSetting AppSetting in AppSettings)
                AppSetting.UserId = userid;
            return base.Update(AppSettings);
        }

        public Response Delete(AppSetting AppSetting, Guid? userid)
        {
            AppSetting.UserId = userid;
            return base.Delete(AppSetting);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new AppSetting() { TableRowGuid = id }, userid);
        }

      
    }
}
