using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class PeriodSvc : EntityBaseSvc<Period>, IComboEntitySvc<Period, Period, Period>
    {
        readonly SessionSvc sessionSvc;

        public PeriodSvc(ILogger logger)
            : base(logger, "Period")
        {
            sessionSvc = new SessionSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Periods
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Session.SessionName+" at "+m.PeriodName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Periods
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Session.SessionName + " at " + m.PeriodName
                    };

            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItemsByPeriods()
        {
            var q = from m in Db.Periods
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Session.SessionName + " from " + m.TimeFrom + " to " + m.TimeTo
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetComboItemsByPeriods(SMSEntities db)
        {
            var q = from m in db.Periods
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Session.SessionName + " from " + m.TimeFrom + " to " + m.TimeTo
                    };
            return q.AsEnumerable();
        }

        public Period GetServiceFilters()
        {
            Period Period = new Period();
            return new Period();
        }
        public IQueryable<Period> Get()
        {
            return Db.Periods;
        }
        public Period GetModel(Guid? userId)
        {           
            return new Period()
            {
                Sessions = sessionSvc.GetComboItems()
            };
        }
        public Period GetModelByUser(Guid? userId)
        {
            var period = this.Get(userId);
            return new Period
            {
                Sessions = sessionSvc.GetComboItems()
            };
        }
        public Period GetModel(Guid id)
        {
            var Period = this.Get(id);

            Period.Sessions = sessionSvc.GetComboItems(Db);

            return Period;
        }
        public Period GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }
        public Period Get(Guid id)
        {
            return Db.Periods.SingleOrDefault(e => e.TableRowGuid == id);
        }
        public Period Get(Guid? userId)
        {
            return Db.Periods.SingleOrDefault(e => e.TableRowGuid == userId);
        }

        public JsTable<Period> Get(PageDetail pageDetail)
        {
            var query = Db.Periods.AsQueryable();

            List<Expression<Func<Period, bool>>> filters = new List<Expression<Func<Period, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.PeriodName.Contains(pageDetail.Search));


            Func<Period, Period> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public JsTable<vw_PeriodList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_PeriodList.AsQueryable();

            List<Expression<Func<vw_PeriodList, bool>>> filters = new List<Expression<Func<vw_PeriodList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.periodView.Contains(pageDetail.Search));


            Func<vw_PeriodList, vw_PeriodList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(Period Period)
        {
            if (string.IsNullOrWhiteSpace(Period.SessionGuid.ToString()))
                return new FailedResponse("Please provide a valid Session Name.");
            if (string.IsNullOrWhiteSpace(Period.PeriodName))
                return new FailedResponse("Please provide a valid Period Name.");
            if ((Period.PeriodName.Any(char.IsSymbol)) || (Period.PeriodName.Any(char.IsPunctuation)))
                return new FailedResponse("Period Name must be Letter or Number.");

            if (Period.TableRowGuid == Guid.Empty)
            {
                var queryPeriod = Db.Periods.Where(r => r.PeriodName == Period.PeriodName);
                var querySession = Db.Periods.Where(r => r.SessionGuid == Period.SessionGuid);
                if ((queryPeriod.ToList().Count > 0) && (querySession.ToList().Count > 0))
                    return new FailedResponse("A Period provided has already been created. Please provide a different Period Name.");
            }
            else
            {
                var queryPeriod = Db.Periods.Where(r => r.PeriodName == Period.PeriodName && r.TableRowGuid != Period.TableRowGuid);
                var querySession = Db.Periods.Where(r => r.SessionGuid == Period.SessionGuid && r.TableRowGuid != Period.TableRowGuid);
                if ((queryPeriod.ToList().Count > 0) && (querySession.ToList().Count > 0))
                    return new FailedResponse("An Period provided has already been created. Please provide a different Period Name.");
            }

            return new SuccessResponse("");
        }
        public Response Add(Period Period, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Period);
            if (!response.Success) return response;

            Period.TableRowGuid = Guid.NewGuid();
            Period.UserId = userid.Value;

  
                        

            return base.Add(Period);
        }
        public Response Update(Period Period, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Period);
            if (!response.Success) return response;

            Period.UserId = userid.Value;
            
             return base.Update(Period);            
        }
        public Response Delete(Period Period, Guid? userid)
        {
            Period.UserId = userid.Value;
            return base.Delete(Period);
        }
        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new Period() { TableRowGuid = id }, userid);
        }


    }
}
