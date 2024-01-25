using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class GroupNameSvc : EntityBaseSvc<GroupName>, IComboEntitySvc<GroupName, GroupName, GroupName>
    {
        readonly LessonClassSvc LessonClassSvc;
        readonly SubjectTeacherClassRoomSvc subjectteacherclassroomSvc;

        public GroupNameSvc(ILogger logger)
            : base(logger, "GroupName")
        {
            LessonClassSvc = new LessonClassSvc(logger);
            subjectteacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.GroupNames
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupName1
                    };

            return q.AsEnumerable();
        }      
        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.GroupNames
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupName1
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetGroupNamesBySubjectTeacherClassRoomId(Guid subjectteacherclassroomGuid)
        {
            var q = from m in Db.GroupNames
                    join stc in Db.SubjectTeacherClassRooms on m.SubjectTeacherClassRoomGuid equals stc.TableRowGuid
                    where (stc.TableRowGuid == subjectteacherclassroomGuid)
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupName1
                    };
            return q.AsEnumerable();
        }
        public IEnumerable<ComboItem> GetGroupNamesBySubjectTeaacherClassRoomGuid(Guid SubjectTeacherClassRoomGuid)
        {
            var q = from m in Db.GroupNames
                    join stc in Db.SubjectTeacherClassRooms on m.SubjectTeacherClassRoomGuid equals stc.TableRowGuid
                    where stc.TableRowGuid == SubjectTeacherClassRoomGuid
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupName1
                    };
            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetGroupNamesByTeacherUserId(string userId)
        {
            Guid userGuid = Guid.Parse(userId);
            Guid TeacherGuid = Db.Teachers.Where(m => m.LogInUserId == userGuid).Select(x => x.TableRowGuid).Single();

            var q = from m in Db.GroupNames
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    join s in Db.Subjects on m.SubjectTeacherClassRoom.SubjectGuid equals s.TableRowGuid
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    where t.TableRowGuid == TeacherGuid
                    select new ComboItem()
                    {
                        //Value = m.TableRowGuid,
                        //Text = m.GroupName + " for " + 
                    };

             return q.AsQueryable();

        }

        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid userId, Guid classroomId)
        {
            Guid TeacherGuid = Db.Teachers.Where(m => m.LogInUserId == userId).Select(x => x.TableRowGuid).Single();

            var q = from m in Db.GroupNames
                    join t in Db.Teachers on m.SubjectTeacherClassRoom.TeacherGuid equals t.TableRowGuid
                    join c in Db.ClassRooms on m.SubjectTeacherClassRoom.ClassRoomGuid equals c.TableRowGuid
                    where (c.TableRowGuid == classroomId)
                    && (t.TableRowGuid == TeacherGuid)
                    select new ComboItem()
                    {
                        Value = m.SubjectTeacherClassRoomGuid,
                        Text = m.SubjectTeacherClassRoom.Subject.SubjectName
                    };
            return q.AsQueryable();
        }

        public GroupName GetServiceFilters()
        {
            GroupName GroupName = new GroupName();
            return new GroupName();
        }

        public IQueryable<GroupName> Get()
        {
            return Db.GroupNames;
        }

        public GroupName GetModel(Guid? userId)
        {
            return new GroupName()
            {
                //LessonClasses = LessonClassSvc.GetComboItems()
            };

        }

        public GroupName GetModelByUser(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();

            return new GroupName()
            {
                ClassRooms = subjectteacherclassroomSvc.GetClassRoomsByTeacherGuid(teacherGuid)
            };

        }

        public GroupName GetModel(Guid id)
        {
            var GroupName = this.Get(id);

            Guid TeacherGuid = Db.GroupNames.Where(x => x.TableRowGuid == id).Select(x => x.SubjectTeacherClassRoom.TeacherGuid).Single();
            GroupName.ClassRoomGuid = (from c in Db.SubjectTeacherClassRooms
                                         where c.TableRowGuid == GroupName.SubjectTeacherClassRoomGuid
                                         select c.ClassRoomGuid).Single();
            GroupName.ClassRooms = subjectteacherclassroomSvc.GetClassRoomsByTeacherGuid(TeacherGuid);
            GroupName.subjectteacherclassroomGuid = (from stc in Db.SubjectTeacherClassRooms
                                                       where stc.TableRowGuid == GroupName.SubjectTeacherClassRoomGuid
                                                 select stc.ClassRoomGuid).Single();
            GroupName.Subjects = subjectteacherclassroomSvc.GetsubjectsByClassRoomId(TeacherGuid, GroupName.subjectteacherclassroomGuid);
            return GroupName;
        }

        public GroupName GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }

        public GroupName Get(Guid id)
        {
            return Db.GroupNames.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<GroupName> Get(PageDetail pageDetail)
        {
            var query = Db.GroupNames.AsQueryable();

            List<Expression<Func<GroupName, bool>>> filters = new List<Expression<Func<GroupName, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.GroupName1.Contains(pageDetail.Search));


            Func<GroupName, GroupName> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_GroupNameList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_GroupNameList.AsQueryable();

            List<Expression<Func<vw_GroupNameList, bool>>> filters = new List<Expression<Func<vw_GroupNameList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.GroupName.Contains(pageDetail.Search));

            filters.Add(x => x.SubjectTeacherClassRoomGuid == pageDetail.SubjectTeacherClassRoomGuid);

            Func<vw_GroupNameList, vw_GroupNameList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }        

        public Response Validate(GroupName GroupName)
        {
            if (string.IsNullOrWhiteSpace(GroupName.GroupName1))
                return new FailedResponse("It's Null Value .");

            if ((GroupName.GroupName1.Any(char.IsNumber)) || (GroupName.GroupName1.Any(char.IsSymbol)) || (GroupName.GroupName1.Any(char.IsPunctuation)))
                return new FailedResponse("Group Name must be letter.");

            if (GroupName.TableRowGuid == Guid.Empty)
            {
                var queryGroupName = Db.GroupNames.Where(r => r.GroupName1 == GroupName.GroupName1);
                var querySubjects = Db.GroupNames.Where(r => r.SubjectTeacherClassRoomGuid == GroupName.SubjectTeacherClassRoomGuid);
                if ((queryGroupName.ToList().Count > 0) && (querySubjects.ToList().Count > 0))
                    return new FailedResponse("A Group Name and Subject provided has already been created. Please provide a different Group Name.");
            }
            else
            {
                var queryGroupName = Db.GroupNames.Where(r => r.GroupName1 == GroupName.GroupName1);
                var querySubjects = Db.GroupNames.Where(r => r.SubjectTeacherClassRoomGuid == GroupName.SubjectTeacherClassRoomGuid);
                if ((queryGroupName.ToList().Count > 0) && (querySubjects.ToList().Count > 0))
                    return new FailedResponse("A Group Name and Subject provided has already been created. Please provide a different Group Name.");
            }

            return new SuccessResponse("");
        }

        public Response Add(GroupName GroupName, Guid? userid)
        {
            Response response = new Response();

            response = Validate(GroupName);

            if (!response.Success) return response;

            GroupName.TableRowGuid = Guid.NewGuid();
            GroupName.UserId = userid;

            return base.Add(GroupName);
        }           

        public Response Update(GroupName GroupName, Guid? userid)
        {
            Response response = new Response();

            response = Validate(GroupName);
            if (!response.Success) return response;

            GroupName.UserId = userid;

            return base.Update(GroupName);
        }

        public Response Delete(GroupName GroupName, Guid? userid)
        {
            GroupName.UserId = userid;
            return base.Delete(GroupName);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new GroupName() { TableRowGuid = id }, userid);
        }


    }
}
