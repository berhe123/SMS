using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class SubjectSvc : EntityBaseSvc<Subject>, IComboEntitySvc<Subject, Subject, Subject>
    {

        public SubjectSvc(ILogger logger)
            : base(logger, "Subject")
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Subjects
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.SubjectName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Subjects
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.SubjectName
                    };

            return q.AsEnumerable();
        }

        public Subject GetServiceFilters()
        {
            Subject Subject = new Subject();
            return new Subject();
        }

        public IQueryable<Subject> Get()
        {
            return Db.Subjects;
        }

        public Subject GetModel(Guid? userId)
        {
            return new Subject();

        }

        public Subject GetModel(Guid id)
        {
            var Subject = this.Get(id);
            return Subject;
        }

        public Subject GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel(id) : this.GetModel(userId);
        }

        public Subject Get(Guid id)
        {
            return Db.Subjects.SingleOrDefault(e => e.TableRowGuid == id);
        }



        public JsTable<Subject> Get(PageDetail pageDetail)
        {
            var query = Db.Subjects.AsQueryable();

            List<Expression<Func<Subject, bool>>> filters = new List<Expression<Func<Subject, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.SubjectName.Contains(pageDetail.Search));


            Func<Subject, Subject> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(Subject Subject)
        {
            if (string.IsNullOrWhiteSpace(Subject.SubjectName))
                return new FailedResponse("Please provide a valid Subject Name.");

            if (Subject.TableRowGuid == Guid.Empty)
            {
                var query = Db.Subjects.Where(r => r.SubjectName == Subject.SubjectName);
                if (query.ToList().Count > 0)
                    return new FailedResponse("A Subject Name provided has already been created. Please provide a different name.");
            }
            else
            {
                var query = Db.Subjects.Where(r => r.SubjectName == Subject.SubjectName && r.TableRowGuid != Subject.TableRowGuid);
                if (query.ToList().Count > 0)
                    return new FailedResponse("An Subject Name provided has already been created. Please provide a different name.");
            }

            return new SuccessResponse("");
        }

        public Response Add(Subject Subject, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Subject);
            if (!response.Success) return response;

            Subject.TableRowGuid = Guid.NewGuid();
            Subject.UserId = userid;

            return base.Add(Subject);
        }

        public Response Update(Subject Subject, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Subject);
            if (!response.Success) return response;

            Subject.UserId = userid;

            return base.Update(Subject);
        }

        public Response Delete(Subject Subject, Guid? userid)
        {
            Subject.UserId = userid;
            return base.Delete(Subject);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new Subject() { TableRowGuid = id }, userid);
        }


    }
}
