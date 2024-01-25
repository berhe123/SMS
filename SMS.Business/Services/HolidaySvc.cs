using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class HolidaySvc : EntityBaseSvc<Holiday>, IComboEntitySvc<Holiday, Holiday, Holiday>
    {
        public HolidaySvc(ILogger logger)
            : base(logger, "Holiday")
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Holidays
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.HolidayName + " " + m.HolidayDate.ToString()
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Holidays
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.HolidayName + " " + m.HolidayDate.ToString()
                    };

            return q.AsEnumerable();
        }

        public Holiday GetServiceFilters()
        {
            Holiday Holiday = new Holiday();
            return new Holiday();
        }
        public IQueryable<Holiday> Get()
        {
            return Db.Holidays;
        }
        public Holiday GetModel(Guid? userId)
        {
            return new Holiday { };
        }
        public Holiday GetModelByUser(Guid? userId)
        {
            return new Holiday { };
        }
        public Holiday GetModel(Guid id)
        {
            var Holiday = this.Get(id);
            return Holiday;
        }
        public Holiday GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public Holiday Get(Guid id)
        {
            return Db.Holidays.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<Holiday> Get(PageDetail pageDetail)
        {
            var query = Db.Holidays.AsQueryable();

            List<Expression<Func<Holiday, bool>>> filters = new List<Expression<Func<Holiday, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.HolidayName.Contains(pageDetail.Search));


            Func<Holiday, Holiday> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }        
        public Response Validate(Holiday Holiday)
        {
            if (string.IsNullOrWhiteSpace(Holiday.HolidayName))
                return new FailedResponse("Please provide a valid Holiday Name.");

            if ((Holiday.HolidayName.Any(char.IsNumber)) || (Holiday.HolidayName.Any(char.IsSymbol)) || (Holiday.HolidayName.Any(char.IsPunctuation)))
                return new FailedResponse("Holiday Name must be letter.");

            if (string.IsNullOrWhiteSpace(Holiday.HolidayDate.ToString()))
                return new FailedResponse("Please provide a valid Holiday Date.");

            if (Holiday.TableRowGuid == Guid.Empty)
            {
                var queryHoliDayName = Db.Holidays.Where(r => r.HolidayName == Holiday.HolidayName);
                var queryHoliDayDate = Db.Holidays.Where(r => r.HolidayDate == Holiday.HolidayDate);
                if (queryHoliDayName.ToList().Count > 0)
                    return new FailedResponse("A Holiday Name provided has already been created. Please provide a different Name.");
                if(queryHoliDayDate.ToList().Count > 0)
                    return new FailedResponse("A Holiday Date provided has already been created. Please provide a different Date.");
                if ((queryHoliDayDate.ToList().Count > 0) && (queryHoliDayDate.ToList().Count > 0))
                    return new FailedResponse("A Holiday provided has already been created. Please provide a different Holiday.");
            }
            else
            {
                var query = Db.Holidays.Where(r => r.HolidayName == Holiday.HolidayName && r.TableRowGuid != Holiday.TableRowGuid);
                if (query.ToList().Count > 0)
                    return new FailedResponse("An Holiday Name provided has already been created. Please provide a different name.");
            }

            return new SuccessResponse("");
        }
        public Response Add(Holiday Holiday, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Holiday);
            if (!response.Success) return response;

            Holiday.TableRowGuid = Guid.NewGuid();
            Holiday.UserId = userid;           

            return base.Add(Holiday);
        }
        public Response Update(Holiday Holiday, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Holiday);
            if (!response.Success) return response;

            Holiday.UserId = userid;
            
             return base.Update(Holiday);            
        }
        public Response Delete(Holiday Holiday, Guid? userid)
        {
            Holiday.UserId = userid;
            return base.Delete(Holiday);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new Holiday() { TableRowGuid = id }, userid);
        }


    }
}
