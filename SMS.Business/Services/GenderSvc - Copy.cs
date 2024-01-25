using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{

    public class GenderSvc : ReadOnlyEntityBaseSvc<Gender>, IReadOnlyEntitySvc<Gender,Gender,Gender>
    {
        //SMSEntities db = new SMSEntities();
        public GenderSvc(ILogger logger)
            : base(logger)
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Genders
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GenderName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Genders
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GenderName
                    };

            return q.AsEnumerable();
        }

        public Gender GetServiceFilters()
        {
            Gender Gender = new Gender();
            return new Gender();
        }

        public IQueryable<Gender> Get()
        {
            return Db.Genders;
        }

        public Gender GetModel()
        {
            return new Gender();

        }

        public Gender GetModel(Guid id)
        {
            var Gender = this.Get(id);
            return Gender;
        }

        public Gender GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel() : this.GetModel(id);
        }

        public Gender Get(Guid id)
        {
            return Db.Genders.SingleOrDefault(e => e.TableRowGuid == id);
        }



        public JsTable<Gender> Get(PageDetail pageDetail)
        {
            var query = Db.Genders.AsQueryable();

            List<Expression<Func<Gender, bool>>> filters = new List<Expression<Func<Gender, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.GenderName.Contains(pageDetail.Search));


            Func<Gender, Gender> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }    

    }
}
