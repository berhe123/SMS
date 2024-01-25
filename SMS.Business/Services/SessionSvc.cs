using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{

    public class SessionSvc : ReadOnlyEntityBaseSvc<Session>, IReadOnlyEntitySvc<Session,Session,Session>
    {
        //SMSEntities db = new SMSEntities();
        public SessionSvc(ILogger logger)
            : base(logger)
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Sessions.OrderBy(m=> m.SessionSort)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.SessionName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Sessions.OrderBy(m=> m.SessionSort)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.SessionName
                    };

            return q.AsEnumerable();
        }

        public Session GetServiceFilters()
        {
            Session Session = new Session();
            return new Session();
        }

        public IQueryable<Session> Get()
        {
            return Db.Sessions;
        }

        public Session GetModel()
        {
            return new Session();

        }

        public Session GetModel(Guid id)
        {
            var Session = this.Get(id);
            return Session;
        }

        public Session GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel() : this.GetModel(id);
        }

        public Session Get(Guid id)
        {
            return Db.Sessions.SingleOrDefault(e => e.TableRowGuid == id);
        }



        public JsTable<Session> Get(PageDetail pageDetail)
        {
            var query = Db.Sessions.AsQueryable();

            List<Expression<Func<Session, bool>>> filters = new List<Expression<Func<Session, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.SessionName.Contains(pageDetail.Search));


            Func<Session, Session> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
       
    }
}
