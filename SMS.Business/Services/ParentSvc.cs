using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class ParentSvc : EntityBaseSvc<Parent>, IComboEntitySvc<Parent, Parent, Parent>
    {

        public ParentSvc(ILogger logger)
            : base(logger, "Parent")
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Parents
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Fullname
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Parents
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Fullname
                    };

            return q.AsEnumerable();
        }

        public Parent GetServiceFilters()
        {
            Parent Parent = new Parent();
            return new Parent();
        }

        public IQueryable<Parent> Get()
        {
            return Db.Parents;
        }

        public Parent GetModel(Guid? userId)
        {
            return new Parent();

        }

        public Parent GetModel(Guid id)
        {
            var Parent = this.Get(id);
            return Parent;
        }

        public Parent GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel(id) : this.GetModel(userId);
        }

        public Parent Get(Guid id)
        {
            return Db.Parents.SingleOrDefault(e => e.TableRowGuid == id);
        }



        public JsTable<Parent> Get(PageDetail pageDetail)
        {
            var query = Db.Parents.AsQueryable();

            List<Expression<Func<Parent, bool>>> filters = new List<Expression<Func<Parent, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.Fullname.Contains(pageDetail.Search));


            Func<Parent, Parent> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(Parent Parent)
        {
            if (string.IsNullOrWhiteSpace(Parent.Fullname))
                return new FailedResponse("Please provide a valid Full Name.");

            if (string.IsNullOrWhiteSpace(Parent.Address))
                return new FailedResponse("Please provide a valid Address.");

            if (string.IsNullOrWhiteSpace(Parent.MobilePhoneNumber.ToString()))
                return new FailedResponse("Please provide a valid Mobile Phone Number.");

            if (string.IsNullOrWhiteSpace(Parent.ReservedPhoneNumber.ToString()))
                return new FailedResponse("Please provide a valid Home Phone Number.");

            //if (Parent.TableRowGuid == Guid.Empty)
            //{
            //    var query = Db.Parents.Where(r => r.Fullname == Parent.Fullname);
            //    if (query.ToList().Count > 0)
            //        return new FailedResponse("A Parent Name provided has already been created. Please provide a different name.");
            //}
            //else
            //{
            //    var query = Db.Parents.Where(r => r.Fullname == Parent.Fullname && r.TableRowGuid != Parent.TableRowGuid);
            //    if (query.ToList().Count > 0)
            //        return new FailedResponse("An Parent Name provided has already been created. Please provide a different name.");
            //}

            return new SuccessResponse("");
        }

        public Response Add(Parent Parent, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Parent);
            if (!response.Success) return response;

            Parent.TableRowGuid = Guid.NewGuid();
            Parent.UserId = userid;

            return base.Add(Parent);
        }

        public Response Update(Parent Parent, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Parent);
            if (!response.Success) return response;

            Parent.UserId = userid;

            return base.Update(Parent);
        }

        public Response Delete(Parent Parent, Guid? userid)
        {
            Parent.UserId = userid;
            return base.Delete(Parent);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new Parent() { TableRowGuid = id }, userid);
        }


    }
}
