using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{

    public class DaySvc : ReadOnlyEntityBaseSvc<Day>, IReadOnlyEntitySvc<Day,Day,Day>
    {
        //SMSEntities db = new SMSEntities();
        public DaySvc(ILogger logger)
            : base(logger)
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Days.OrderBy(m => m.DaySort)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.DayName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Days.OrderBy(m => m.DaySort)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.DayName
                    };

            return q.AsEnumerable();
        }

        public Day GetServiceFilters()
        {
            Day Day = new Day();
            return new Day();
        }

        public IQueryable<Day> Get()
        {
            return Db.Days;
        }

        public Day GetModel()
        {
            return new Day();

        }

        public Day GetModel(Guid id)
        {
            var Day = this.Get(id);
            return Day;
        }

        public Day GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel() : this.GetModel(id);
        }

        public Day Get(Guid id)
        {
            return Db.Days.SingleOrDefault(e => e.TableRowGuid == id);
        }



        public JsTable<Day> Get(PageDetail pageDetail)
        {
            var query = Db.Days.AsQueryable();

            List<Expression<Func<Day, bool>>> filters = new List<Expression<Func<Day, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.DayName.Contains(pageDetail.Search));


            Func<Day, Day> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
       
    }
}
