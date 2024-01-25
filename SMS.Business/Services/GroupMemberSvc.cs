using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class GroupMemberSvc : EntityBaseSvc<GroupMember>, IComboEntitySvc<GroupMember, GroupMember, GroupMember>
    {
        readonly SubjectTeacherClassRoomSvc subjectteacherclassroomSvc;
        public GroupMemberSvc(ILogger logger)
            : base(logger, "GroupMember")
        {
            subjectteacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.GroupMembers
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupName.GroupName1
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.GroupMembers
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupName.GroupName1
                    };

            return q.AsEnumerable();
        }

        public GroupMember GetServiceFilters()
        {
            GroupMember StudentsInGroupMember = new GroupMember();
            return new GroupMember();
        }

        public IQueryable<GroupMember> Get()
        {
            return Db.GroupMembers;
        }

        public GroupMember GetModel(Guid? userId)
        {
            return new GroupMember()
            {
                //LessonClasses = LessonClassSvc.GetComboItems()
            };

        }

        public GroupMember GetModelByUser(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();

            return new GroupMember()
            {
                ClassRooms = subjectteacherclassroomSvc.GetClassRoomsByTeacherGuid(teacherGuid)
            };

        }

        public GroupMember GetModel(Guid id)
        {
            var GroupMember = this.Get(id);
            
            return GroupMember;
        }

        public GroupMember GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }

        public GroupMember Get(Guid id)
        {
            return Db.GroupMembers.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<GroupMember> Get(PageDetail pageDetail)
        {
            var query = Db.GroupMembers.AsQueryable();

            List<Expression<Func<GroupMember, bool>>> filters = new List<Expression<Func<GroupMember, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.GroupName.GroupName1.Contains(pageDetail.Search));


            Func<GroupMember, GroupMember> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(GroupMember GroupMember)
        {           
            return new SuccessResponse("");
        }

        public Response Add(GroupMember GroupMember, Guid? userid)
        {
            Response response = new Response();

            response = Validate(GroupMember);

            if (!response.Success) return response;

            GroupMember.TableRowGuid = Guid.NewGuid();
            GroupMember.UserId = userid;

            return base.Add(GroupMember);
        }

        public Response Update(GroupMember GroupMember, Guid? userid)
        {
            Response response = new Response();

            response = Validate(GroupMember);
            if (!response.Success) return response;

            GroupMember.UserId = userid;

            return base.Update(GroupMember);
        }

        public Response Delete(GroupMember GroupMember, Guid? userid)
        {
            GroupMember.UserId = userid;
            return base.Delete(GroupMember);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new GroupMember() { TableRowGuid = id }, userid);
        }


    }
}
