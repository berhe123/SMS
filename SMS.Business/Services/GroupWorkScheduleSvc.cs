using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class GroupWorkScheduleSvc : EntityBaseSvc<GroupWorkSchedule>, IComboEntitySvc<GroupWorkSchedule, GroupWorkSchedule, GroupWorkSchedule>
    {
        readonly LessonClassSvc LessonClassSvc;
        readonly SubjectTeacherClassRoomSvc subjectteacherclassroomSvc;
        readonly GroupNameSvc groupnameSvc;

        public GroupWorkScheduleSvc(ILogger logger)
            : base(logger, "GroupWorkSchedule")
        {
            LessonClassSvc = new LessonClassSvc(logger);
            subjectteacherclassroomSvc = new SubjectTeacherClassRoomSvc(logger);
            groupnameSvc = new GroupNameSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.GroupWorkSchedules
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupWorkTitle
                    };

            return q.AsEnumerable();
        }      

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.GroupWorkSchedules
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupWorkTitle
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetGroupNamesByLessonClassId()
        {
            Guid subjectteacherclassroomId = new Guid();

            var q = from m in Db.GroupWorkSchedules
                    join stc in Db.SubjectTeacherClassRooms on m.SubjectTeacherClassRoomGuid equals stc.TableRowGuid
                    join gn in Db.GroupMembers on m.GroupNameGuid equals gn.TableRowGuid
                    where stc.TableRowGuid == subjectteacherclassroomId
                    select new ComboItem
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupWorkTitle + " for " + m.GroupName.GroupName1 + " => " + m.OutOf.ToString()
                    };
            return q.AsQueryable();
        }
        public IEnumerable<ComboItem> GetGroupNamesByLessonClassId(Guid subjectteacherclassroomId)
        {
            var q = from m in Db.GroupWorkSchedules
                    join stc in Db.SubjectTeacherClassRooms on m.SubjectTeacherClassRoomGuid equals stc.TableRowGuid
                    join gn in Db.GroupMembers on m.GroupNameGuid equals gn.TableRowGuid
                    where stc.TableRowGuid == subjectteacherclassroomId
                    select new ComboItem
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupWorkTitle + " for " + m.GroupName.GroupName1 + " => " + m.OutOf.ToString()
                    };
            return q.AsQueryable();
        }

        public GroupWorkSchedule GetServiceFilters()
        {
            GroupWorkSchedule GroupWorkSchedule = new GroupWorkSchedule();
            return new GroupWorkSchedule();
        }

        public IQueryable<GroupWorkSchedule> Get()
        {
            return Db.GroupWorkSchedules;
        }

        public GroupWorkSchedule GetModel(Guid? userId)
        {
            return new GroupWorkSchedule()
            {
                //LessonClasses = LessonClassSvc.GetComboItems()
            };

        }
        public GroupWorkSchedule GetModelByUser(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();
            return new GroupWorkSchedule()
            {
                ClassRoomSubjects = subjectteacherclassroomSvc.GetSubjectClassRoomsByTeacherGuid(teacherGuid)
            };
        }

        public GroupWorkSchedule GetModel(Guid id)
        {
            var GroupWorkSchedule = this.Get(id);
            Guid TeacherGuid = Db.GroupWorkSchedules.Where(x => x.TableRowGuid == id).Select(x => x.SubjectTeacherClassRoom.TeacherGuid).Single();

            GroupWorkSchedule.ClassRoomSubjects = subjectteacherclassroomSvc.GetSubjectClassRoomsByTeacherGuid(TeacherGuid);
            GroupWorkSchedule.subjectteacherclassroomGuid = (from stc in Db.SubjectTeacherClassRooms
                                                 where stc.TableRowGuid == GroupWorkSchedule.SubjectTeacherClassRoomGuid
                                                 select stc.TableRowGuid).Single();
            GroupWorkSchedule.GroupNames = groupnameSvc.GetGroupNamesBySubjectTeacherClassRoomId(GroupWorkSchedule.subjectteacherclassroomGuid);
            return GroupWorkSchedule;
        }

        public GroupWorkSchedule GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUser(userId) : this.GetModel(id);
        }

        public GroupWorkSchedule Get(Guid id)
        {
            return Db.GroupWorkSchedules.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<GroupWorkSchedule> Get(PageDetail pageDetail)
        {
            var query = Db.GroupWorkSchedules.AsQueryable();

            List<Expression<Func<GroupWorkSchedule, bool>>> filters = new List<Expression<Func<GroupWorkSchedule, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.GroupWorkTitle+m.GroupWorkTitle+m.GroupWorkGivenDate+m.GroupWorkSubmissionDate).Contains(pageDetail.Search));


            Func<GroupWorkSchedule, GroupWorkSchedule> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_GroupWorkScheduleList> GetList(PageDetail pageDetail)
        {
            var query = Db.vw_GroupWorkScheduleList.AsQueryable();

            List<Expression<Func<vw_GroupWorkScheduleList, bool>>> filters = new List<Expression<Func<vw_GroupWorkScheduleList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => m.GroupWork.Contains(pageDetail.Search));

            filters.Add(x => x.SubjectTeacherClassRoomGuid == pageDetail.SubjectTeacherClassRoomGuid);

            Func<vw_GroupWorkScheduleList, vw_GroupWorkScheduleList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        //public JsTable<GroupNames> GetGroupNamesByLessonClassId(PageDetail pageDetail, Guid subjectteacherclassroomId)
        //{
        //    var q = (from m in Db.GroupWorkSchedules
        //             join g in Db.GroupMembers on m.GroupMemberGuid equals g.TableRowGuid
        //             join stc in Db.LessonClasses on m.SubjectTeacherClassRoomGuid equals stc.TableRowGuid
        //             where stc.TableRowGuid == subjectteacherclassroomId
        //             select new GroupNames()
        //             {
        //                 TableRowGuid = m.gro
        //                 GroupName = m.GroupWorkTitle + " for " + g.GroupName + " => " + m.OutOf.ToString()
        //             }).AsQueryable();

        //    List<Expression<Func<GroupNames, bool>>> filters = new List<Expression<Func<GroupNames, bool>>>();

        //    //if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.ClassRoomName.Teacher.FirstName + m.SubjectTeacherClassRoom.Teacher.FatherName + m.SubjectTeacherClassRoom.Teacher.GrandFatherName + m.SubjectTeacherClassRoom.Subject.SubjectName + m.SubjectTeacherClassRoom.ClassRoom.ClassRoomName + m.Day.DayName + m.Period.PeriodName + m.Period.Session.SessionName).Contains(pageDetail.Search));

        //    Func<GroupNames, GroupNames> result = e => e;

        //    return GridQueryHelper.JsTable(q, pageDetail, filters, result);
        //}  

        public Response Validate(GroupWorkSchedule GroupWorkSchedule)
        {
            //if (string.IsNullOrWhiteSpace(GroupWorkSchedule.GroupWorkTitle))
            //    return new FailedResponse("Please provide a valid group work title name.");

            //if (string.IsNullOrWhiteSpace(GroupWorkSchedule.GroupMembers))
            //    return new FailedResponse("Please provide a valid group members name.");

            //if (GroupWorkSchedule.TableRowGuid == Guid.Empty)
            //{
            //    var queryGroupTitle = Db.GroupWorkSchedules.Where(r => r.GroupWorkTitle == GroupWorkSchedule.GroupWorkTitle);
            //    var queryGroupMembers = Db.GroupWorkSchedules.Where(r => r.GroupMembers == GroupWorkSchedule.GroupMembers);
            //    if ((queryGroupTitle.ToList().Count > 0) && (queryGroupMembers.ToList().Count > 0))
            //        return new FailedResponse("A Group Work Title and Member provided has already been created. Please provide a different Title and Member.");
            //}
            //else
            //{
            //    var queryGroupTitle = Db.GroupWorkSchedules.Where(r => r.GroupWorkTitle == GroupWorkSchedule.GroupWorkTitle && r.TableRowGuid != GroupWorkSchedule.TableRowGuid);
            //    var queryGroupMembers = Db.GroupWorkSchedules.Where(r => r.GroupMembers == GroupWorkSchedule.GroupMembers && r.TableRowGuid != GroupWorkSchedule.TableRowGuid);
            //    if ((queryGroupTitle.ToList().Count > 0) && (queryGroupMembers.ToList().Count > 0))
            //        return new FailedResponse("An Group Work Title and Member provided has already been created. Please provide a different Title and Member.");
            //}

            return new SuccessResponse("");
        }

        public Response Add(GroupWorkSchedule GroupWorkSchedule, Guid? userid)
        {
            Response response = new Response();

            response = Validate(GroupWorkSchedule);
            if (!response.Success) return response;

            GroupWorkSchedule.TableRowGuid = Guid.NewGuid();
            GroupWorkSchedule.UserId = userid;

            return base.Add(GroupWorkSchedule);
        }

        public Response Update(GroupWorkSchedule GroupWorkSchedule, Guid? userid)
        {
            Response response = new Response();

            response = Validate(GroupWorkSchedule);
            if (!response.Success) return response;

            GroupWorkSchedule.UserId = userid;

            return base.Update(GroupWorkSchedule);
        }

        public Response Delete(GroupWorkSchedule GroupWorkSchedule, Guid? userid)
        {
            GroupWorkSchedule.UserId = userid;
            return base.Delete(GroupWorkSchedule);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new GroupWorkSchedule() { TableRowGuid = id }, userid);
        }


    }
}
