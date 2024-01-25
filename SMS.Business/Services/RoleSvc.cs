using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class RoleSvc : ReadOnlyEntityBaseSvc<security_Roles>, IReadOnlyEntitySvc<security_Roles, security_Roles, security_Roles>
    {

        public RoleSvc(ILogger logger)
            : base(logger)
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.security_Roles
                    select new ComboItem()
                    {
                        Value = m.RoleId,
                        Text = m.RoleName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.security_Roles
                    select new ComboItem()
                    {
                        Value = m.RoleId,
                        Text = m.RoleName
                    };

            return q.AsEnumerable();
        }

        public security_Roles GetServiceFilters()
        {
            security_Roles security_Roles = new security_Roles();
            return new security_Roles();
        }

        public IQueryable<security_Roles> Get()
        {
            return Db.security_Roles;
        }

        public security_Roles GetModel()
        {
            return new security_Roles();

        }

        public security_Roles GetModel(Guid id)
        {
            var security_Roles = this.Get(id);
            return security_Roles;
        }

        public security_Roles GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel() : this.GetModel(id);
        }

        public security_Roles Get(Guid id)
        {
            return Db.security_Roles.SingleOrDefault(e => e.RoleId == id);
        }

        public JsTable<security_Roles> Get(PageDetail pageDetail)
        {
            var query = Db.security_Roles.AsQueryable();

            List<Expression<Func<security_Roles, bool>>> filters = new List<Expression<Func<security_Roles, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.RoleName.Contains(pageDetail.Search));


            Func<security_Roles, security_Roles> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
        public Guid[] GetUserRoles(Guid id)
        {
            var user = (from u in Db.UserProfiles.Include("security_Roles").Where(x => x.UserId == id)
                        select u).SingleOrDefault();

            var roleGuids = from r in user.security_Roles
                            select r.RoleId;

            return roleGuids.ToArray();

        }
    }
}
