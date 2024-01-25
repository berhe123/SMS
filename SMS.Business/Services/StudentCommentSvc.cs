using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class StudentCommentCommentSvc : EntityBaseSvc<StudentComment>, IComboEntitySvc<StudentComment, StudentComment, StudentComment>
    {
        public StudentCommentCommentSvc(ILogger logger)
            : base(logger, "StudentComment")    
        {

        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.StudentComments
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.StudentComments
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public StudentComment GetServiceFilters()
        {
            StudentComment StudentComment = new StudentComment();
            return new StudentComment();
        }

        public IQueryable<StudentComment> Get()
        {
            return Db.StudentComments;
        }

        public StudentComment GetModel(Guid? userId)
        {
            return new StudentComment
            {
                
            };

        }

        public StudentComment GetModel(Guid id)
        {
            var StudentComment = this.Get(id);           

            return StudentComment;
        }

        public StudentComment GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModel(id) : this.GetModel(userId);
        }

        public StudentComment Get(Guid id)
        {
            return Db.StudentComments.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<StudentComment> Get(PageDetail pageDetail)
        {
            var query = Db.StudentComments.AsQueryable();

            List<Expression<Func<StudentComment, bool>>> filters = new List<Expression<Func<StudentComment, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.Student.FirstName + " " + m.Student.FatherName + " " + m.Student.GrandFatherName).Contains(pageDetail.Search));


            Func<StudentComment, StudentComment> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }
       
        public Response Validate(StudentComment StudentComment)
        {           
            //if (char.MaxValue.Equals(5))
            //{
            //    return new FailedResponse("Please provide a letter only for First Name.");
            //}

            //if (StudentComment.TableRowGuid == Guid.Empty)
            //{
            //    var query = Db.StudentComments.Where(r => r.StudentCommentName == StudentComment.StudentCommentName);
            //    if (query.ToList().Count > 0)
            //        return new FailedResponse("A StudentComment Name provided has already been created. Please provide a different name.");
            //}
            //else
            //{
            //    var query = Db.StudentComments.Where(r => r.StudentCommentName == StudentComment.StudentCommentName && r.TableRowGuid != StudentComment.TableRowGuid);
            //    if (query.ToList().Count > 0)
            //        return new FailedResponse("An StudentComment Name provided has already been created. Please provide a different name.");
            //}

            return new SuccessResponse("");
        }

        public Response Add(StudentComment StudentComment, Guid? userid)
        {
            Response response = new Response();

            response = Validate(StudentComment);
            if (!response.Success) return response;

            StudentComment.TableRowGuid = Guid.NewGuid();
            StudentComment.UserId = userid;            

            return base.Add(StudentComment);
        }

        public Response Update(StudentComment StudentComment, Guid? userid)
        {
            Response response = new Response();

            response = Validate(StudentComment);
            if (!response.Success) return response;

            StudentComment.UserId = userid;
            
             return base.Update(StudentComment);            
        }

        public Response Delete(StudentComment StudentComment, Guid? userid)
        {
            StudentComment.UserId = userid;
            return base.Delete(StudentComment);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new StudentComment() { TableRowGuid = id }, userid);
        }


    }
}
