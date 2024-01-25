using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class TeacherSvc : EntityBaseSvc<Teacher>, IComboEntitySvc<Teacher, Teacher, Teacher>
    {
        readonly GenderSvc genderSvc;
        readonly UserProfileSvc userProfileSvc;
        public TeacherSvc(ILogger logger)
            : base(logger, "Teacher")
        {
            genderSvc = new GenderSvc(logger);
            userProfileSvc = new UserProfileSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.Teachers
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.FirstName + " " + m.FatherName + " " + m.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.Teachers
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.FirstName + " " + m.FatherName + " " + m.GrandFatherName
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(Guid subjectteacherclassroomGuid)
        {
            Guid teacherGuid = Db.SubjectTeacherClassRooms.Where(r => r.TableRowGuid == subjectteacherclassroomGuid).Select(x => x.TeacherGuid).Single();

            var q = from m in Db.Teachers
                    where m.TableRowGuid != teacherGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.FirstName + " " + m.FatherName + " " + m.GrandFatherName
                    };
            return q.AsEnumerable();
        }

        public Teacher GetServiceFilters()
        {
            Teacher Teacher = new Teacher();
            return new Teacher();
        }

        public IQueryable<Teacher> Get()
        {
            return Db.Teachers;
        }

        public Teacher GetModel(Guid? userId)
        {
            return new Teacher {
                Genders = genderSvc.GetComboItems(),
                Users =  userProfileSvc.GetTeacherComboItems()
            };

        }

        public Teacher GetModelByUserId(Guid? userId)
        {
            return new Teacher
            {
                Genders = genderSvc.GetComboItems(),
                Users = userProfileSvc.GetTeacherComboItems()
            };

        }

        public Teacher GetModel(Guid id)
        {
            var Teacher = this.Get(id);
            Teacher.Genders = genderSvc.GetComboItems(Db);
            Teacher.Users = userProfileSvc.GetTeacherComboItems(Db);
            return Teacher;
        }

        public Guid[] GetClassRooms(Guid id)
        {
            var GetClassRooms = (from c in Db.SubjectTeacherClassRooms
                                 where c.TeacherGuid == id
                                 select (Guid)c.ClassRoomGuid);
            return GetClassRooms.ToArray();
        }

        public Teacher GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUserId(userId) : this.GetModel(id);
        }

        public Teacher Get(Guid id)
        {
            return Db.Teachers.SingleOrDefault(e => e.TableRowGuid == id);
        }
        public JsTable<Teacher> Get(PageDetail pageDetail)
        {
            var query = Db.Teachers.AsQueryable();

            List<Expression<Func<Teacher, bool>>> filters = new List<Expression<Func<Teacher, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.FirstName.Contains(pageDetail.Search));


            Func<Teacher, Teacher> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_TeacherList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_TeacherList.AsQueryable();

            List<Expression<Func<vw_TeacherList, bool>>> filters = new List<Expression<Func<vw_TeacherList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.TeacherFullName.Contains(pageDetail.Search));


            Func<vw_TeacherList, vw_TeacherList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_TeachersForLessonClassDataView> GetTeachersForLessonClassDataView(PageDetail pageDetail)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == pageDetail.UserId).Select(x => x.TableRowGuid).Single();
            var query = Db.vw_TeachersForLessonClassDataView.AsQueryable();

            List<Expression<Func<vw_TeachersForLessonClassDataView, bool>>> filters = new List<Expression<Func<vw_TeachersForLessonClassDataView, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.lesson.Contains(pageDetail.Search));

            filters.Add(x => x.TeacherGuid == teacherGuid);

            Func<vw_TeachersForLessonClassDataView, vw_TeachersForLessonClassDataView> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }       

        public Response Validate(Teacher Teacher)
        {
            if (string.IsNullOrWhiteSpace(Teacher.FirstName))
                return new FailedResponse("Please provide a valid First Name.");
            if ((Teacher.FirstName.Any(char.IsNumber)) || (Teacher.FirstName.Any(char.IsSymbol)) || (Teacher.FirstName.Any(char.IsPunctuation)))
                return new FailedResponse("First Name must be letter.");
            if ((Teacher.FirstName.Any(char.IsWhiteSpace)))
                return new FailedResponse("Please remove white space from First Name.");

            if (string.IsNullOrWhiteSpace(Teacher.FatherName))
                return new FailedResponse("Please provide a valid Father Name.");
            if ((Teacher.FatherName.Any(char.IsNumber)) || (Teacher.FatherName.Any(char.IsSymbol)) || (Teacher.FatherName.Any(char.IsPunctuation)))
                return new FailedResponse("Father Name must be letter.");
            if ((Teacher.FatherName.Any(char.IsWhiteSpace)))
                return new FailedResponse("Please remove white space from Father Name.");

            if (string.IsNullOrWhiteSpace(Teacher.GrandFatherName))
                return new FailedResponse("Please provide a valid Grand Father Name.");
            if ((Teacher.GrandFatherName.Any(char.IsNumber)) || (Teacher.GrandFatherName.Any(char.IsSymbol)) || (Teacher.GrandFatherName.Any(char.IsPunctuation)))
                return new FailedResponse("Grand Father Name must be letter.");
            if ((Teacher.GrandFatherName.Any(char.IsWhiteSpace)))
                return new FailedResponse("Please remove white space from Grand Father Name.");

            if (string.IsNullOrWhiteSpace(Teacher.BirthDate.ToString()))
                return new FailedResponse("Please provide a valid Brith Date.");

            if (string.IsNullOrWhiteSpace(Teacher.GenderGuid.ToString()))
                return new FailedResponse("Please provide a valid Brith Date.");

            if (string.IsNullOrWhiteSpace(Teacher.Address))
                return new FailedResponse("Please provide a valid Address.");

            if (string.IsNullOrWhiteSpace(Teacher.MobilePhoneNumber.ToString()))
                return new FailedResponse("Please provide a valid Mobile Phone Number.");

            if (string.IsNullOrWhiteSpace(Teacher.ReservedPhoneNumber.ToString()))
                return new FailedResponse("Please provide a valid Reserved Phone Number.");

            if (string.IsNullOrWhiteSpace(Teacher.Salary.ToString()))
                return new FailedResponse("Please provide a valid Salary.");

            if (string.IsNullOrWhiteSpace(Teacher.Experience))
                return new FailedResponse("Please provide a valid Experience.");
            if ((Teacher.Experience.Any(char.IsSymbol)) || (Teacher.Experience.Any(char.IsPunctuation)))
                return new FailedResponse("Experience must be letter and number.");

            if (string.IsNullOrWhiteSpace(Teacher.EngagementDate.ToString()))
                return new FailedResponse("Please provide a valid Engagement Date.");

            if (string.IsNullOrWhiteSpace(Teacher.LogInUserId.ToString()))
                return new FailedResponse("Please provide a valid User Name.");

            if (Teacher.TableRowGuid == Guid.Empty)
            {
                var queryFirstName = Db.Workers.Where(r => r.FirstName == Teacher.FirstName);
                var queryFatherName = Db.Workers.Where(r => r.FatherName == Teacher.FatherName);
                var queryGrandFatherName = Db.Workers.Where(r => r.GrandFatherName == Teacher.GrandFatherName);
                if ((queryFirstName.ToList().Count > 0) && (queryFatherName.ToList().Count > 0) && (queryGrandFatherName.ToList().Count > 0))
                    return new FailedResponse("A Teacher Name provided has already been created. Please provide a different name.");
            }
            else
            {
                var queryFirstName = Db.Workers.Where(r => r.FirstName == Teacher.FirstName && r.TableRowGuid != Teacher.TableRowGuid);
                var queryFatherName = Db.Workers.Where(r => r.FatherName == Teacher.FatherName && r.TableRowGuid != Teacher.TableRowGuid);
                var queryGrandFatherName = Db.Workers.Where(r => r.GrandFatherName == Teacher.GrandFatherName && r.TableRowGuid != Teacher.TableRowGuid);
                if ((queryFirstName.ToList().Count > 0) && (queryFatherName.ToList().Count > 0) && (queryGrandFatherName.ToList().Count > 0))
                    return new FailedResponse("An Teacher Name provided has already been created. Please provide a different name.");
            }

            return new SuccessResponse("");
        }

        public Response Add(Teacher Teacher, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Teacher);
            if (!response.Success) return response;

            Teacher.TableRowGuid = Guid.NewGuid();
            Teacher.UserId = userid;

            return base.Add(Teacher);
        }

        public Response Update(Teacher Teacher, Guid? userid)
        {
            Response response = new Response();

            response = Validate(Teacher);
            if (!response.Success) return response;

            Teacher.UserId = userid;

            return base.Update(Teacher);
        }

        public Response Delete(Teacher Teacher, Guid? userid)
        {
            Teacher.UserId = userid;
            return base.Delete(Teacher);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new Teacher() { TableRowGuid = id }, userid);
        }


    }
}
