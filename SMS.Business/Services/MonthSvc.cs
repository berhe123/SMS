using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{

    public class MonthSvc : ReadOnlyEntityBaseSvc<Month>, IReadOnlyEntitySvc<Month,Month,Month>
    {
        //SMSEntities db = new SMSEntities();
        public MonthSvc(ILogger logger)
            : base(logger)
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Months.OrderBy(m => m.MonthSort)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.MonthName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Months.OrderBy(m=> m.MonthSort)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.MonthName
                    };

            return q.AsEnumerable();
        }

        public Month GetServiceFilters()
        {
            Month Month = new Month();
            return new Month();
        }

        public IQueryable<Month> Get()
        {
            return Db.Months;
        }

        public Month GetModel()
        {
            return new Month();

        }

        public Month GetModel(Guid id)
        {
            var Month = this.Get(id);
            return Month;
        }

        public Month GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel() : this.GetModel(id);
        }

        public Month Get(Guid id)
        {
            return Db.Months.SingleOrDefault(e => e.TableRowGuid == id);
        }



        public JsTable<Month> Get(PageDetail pageDetail)
        {
            var query = Db.Months.AsQueryable();

            List<Expression<Func<Month, bool>>> filters = new List<Expression<Func<Month, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.MonthName.Contains(pageDetail.Search));


            Func<Month, Month> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
       
    }
}
