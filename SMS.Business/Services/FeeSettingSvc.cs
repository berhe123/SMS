using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class FeeSettingSvc : EntityBaseSvc<FeeSetting>, IComboEntitySvc<FeeSetting, FeeSetting, FeeSetting>
    {
        readonly MonthSvc monthSvc;
        readonly ClassRoomSvc classroomSvc;
        public FeeSettingSvc(ILogger logger)
            : base(logger, "FeeSetting")
        {
            monthSvc = new MonthSvc(logger);
            classroomSvc = new ClassRoomSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.FeeSettings
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Amount.ToString()+" is for "+m.ClassRoom.ClassRoomName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.FeeSettings
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Amount.ToString() + " is for " + m.ClassRoom.ClassRoomName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetFeeSettingByPaymentCalanderId()
        {
            Guid MonthId = new Guid();

            var q = from m in Db.FeeSettings
                    where m.MonthGuid == MonthId
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoom.ClassRoomName + " " + m.Amount.ToString() 
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetFeeSettingByPaymentCalanderId(Guid MonthGuid)
        {
            var q = from m in Db.FeeSettings
                    where (m.MonthGuid == MonthGuid)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.ClassRoom.ClassRoomName + " => " + m.Amount.ToString()
                    };
            return q.AsEnumerable();
        }

        public FeeSetting GetServiceFilters()
        {
            FeeSetting FeeSetting = new FeeSetting();
            return new FeeSetting();
        }
        public IQueryable<FeeSetting> Get()
        {
            return Db.FeeSettings;
        }
        public FeeSetting GetModel(Guid? userId)
        {
            return new FeeSetting
            {
                Months = monthSvc.GetComboItems(),
                ClassRooms=classroomSvc.GetComboItems()
            };
        }
        public FeeSetting GetModelByUser(Guid? userId)
        {
            return new FeeSetting
            {
                Months = monthSvc.GetComboItems(),
                ClassRooms = classroomSvc.GetComboItems()
            };
        }
        public FeeSetting GetModel(Guid id)
        {
            var FeeSetting = this.Get(id);
            FeeSetting.Months = monthSvc.GetComboItems(Db);
            FeeSetting.ClassRooms = classroomSvc.GetComboItems(Db);
            return FeeSetting;
        }
        public FeeSetting GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public FeeSetting Get(Guid id)
        {
            return Db.FeeSettings.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<FeeSetting> Get(PageDetail pageDetail)
        {
            var query = Db.FeeSettings.AsQueryable();

            List<Expression<Func<FeeSetting, bool>>> filters = new List<Expression<Func<FeeSetting, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.ClassRoom.ClassRoomName+m.Amount).Contains(pageDetail.Search));


            Func<FeeSetting, FeeSetting> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_FeeSettingList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_FeeSettingList.AsQueryable();

            List<Expression<Func<vw_FeeSettingList, bool>>> filters = new List<Expression<Func<vw_FeeSettingList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.ClassRoomName + m.MonthName).Contains(pageDetail.Search));

            filters.Add(x => x.ClassRoomGuid == pageDetail.ClassRoomGuid);

            Func<vw_FeeSettingList, vw_FeeSettingList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public Response Validate(FeeSetting FeeSetting)
        {
            if (string.IsNullOrWhiteSpace(FeeSetting.ClassRoomGuid.ToString()))
                return new FailedResponse("Please provide a valid Class Room.");

            if (string.IsNullOrWhiteSpace(FeeSetting.MonthGuid.ToString()))
                return new FailedResponse("Please provide a valid Payment Calander.");

            if (string.IsNullOrWhiteSpace(FeeSetting.Amount.ToString()))
                return new FailedResponse("Please provide a valid Amount.");

            if (FeeSetting.TableRowGuid == Guid.Empty)
            {
                var queryMonth = Db.FeeSettings.Where(r => r.MonthGuid == FeeSetting.MonthGuid);
                var queryClassRoom = Db.FeeSettings.Where(r => r.ClassRoomGuid == FeeSetting.ClassRoomGuid);

                if ((queryClassRoom.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
                    return new FailedResponse("A Fee Setting provided has already been created. Please provide a different Fee Setting.");
            }
            else
            {
                var queryClassRoom = Db.FeeSettings.Where(r => r.ClassRoomGuid == FeeSetting.ClassRoomGuid && r.TableRowGuid != FeeSetting.TableRowGuid);
                var queryPaymentCalander = Db.FeeSettings.Where(r => r.MonthGuid == FeeSetting.MonthGuid && r.TableRowGuid != FeeSetting.TableRowGuid);
                if ((queryClassRoom.ToList().Count > 0) && (queryPaymentCalander.ToList().Count > 0))
                    return new FailedResponse("An Fee Setting provided has already been created. Please provide a different Fee Setting.");
            }

            return new SuccessResponse("");
        }
        public Response Add(FeeSetting FeeSetting, Guid? userid)
        {
            Response response = new Response();

            response = Validate(FeeSetting);
            if (!response.Success) return response;

            FeeSetting.TableRowGuid = Guid.NewGuid();
            FeeSetting.UserId = userid;            

            return base.Add(FeeSetting);
        }
        public Response Update(FeeSetting FeeSetting, Guid? userid)
        {
            Response response = new Response();

            response = Validate(FeeSetting);
            if (!response.Success) return response;

            FeeSetting.UserId = userid;
            
             return base.Update(FeeSetting);            
        }
        public Response Delete(FeeSetting FeeSetting, Guid? userid)
        {
            FeeSetting.UserId = userid;
            return base.Delete(FeeSetting);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new FeeSetting() { TableRowGuid = id }, userid);
        }


    }
}
