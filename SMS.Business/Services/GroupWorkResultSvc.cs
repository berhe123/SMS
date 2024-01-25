using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Entities;
using SMS.Core;
using System.Linq.Expressions;

namespace SMS.Business.Service
{
    public class GroupWorkResultSvc : EntityBaseSvc<GroupWorkResult>, IComboEntitySvc<GroupWorkResult, GroupWorkResult, GroupWorkResult>
    {
        readonly StudentSvc studentSvc;
        readonly GroupWorkScheduleSvc groupworkscheduleSvc;
        readonly LessonClassSvc lessonclassSvc;
        public GroupWorkResultSvc(ILogger logger)
            : base(logger, "GroupWorkResult")
        {
            studentSvc = new StudentSvc(logger);
            groupworkscheduleSvc = new GroupWorkScheduleSvc(logger);
            lessonclassSvc = new LessonClassSvc(logger);
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            var q = from m in Db.GroupWorkResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupWorkSchedule.GroupName.GroupName1
                    };

            return q.AsEnumerable();
        }

        public IEnumerable<ComboItem> GetComboItems(SMSEntities db)
        {
            var q = from m in db.GroupWorkResults
                    select new ComboItem()
                    {
                        Value = m.TableRowGuid,
                        Text = m.GroupWorkSchedule.GroupName.GroupName1
                    };

            return q.AsEnumerable();
        }

        public GroupWorkResult GetServiceFilters()
        {
            GroupWorkResult GroupWorkResult = new GroupWorkResult();
            return new GroupWorkResult();
        }

        public IQueryable<GroupWorkResult> Get()
        {
            return Db.GroupWorkResults;
        }

        public GroupWorkResult GetModel(Guid? userId)
        {
            return new GroupWorkResult() {
                //Students = studentSvc.GetComboItems(),

                //GroupWorkSchedules = groupworkscheduleSvc.GetComboItems()

            };

        }

        public GroupWorkResult GetModelByUserId(Guid? userId)
        {
            Guid teacherGuid = Db.Teachers.Where(x => x.LogInUserId == userId.Value).Select(x => x.TableRowGuid).Single();
            return new GroupWorkResult()
            {
                ClassRooms = lessonclassSvc.GetClassRoomSubjectsByTeacherGuid(teacherGuid)

            };

        }

        public GroupWorkResult GetModel(Guid id)
        {
            var GroupWorkResult = this.Get(id);

            //GroupWorkResult.Students = studentSvc.GetComboItems(Db);
            //GroupWorkResult.GroupWorkSchedules = groupworkscheduleSvc.GetComboItems(Db);

            return GroupWorkResult;
        }

        public GroupWorkResult GetModel(Guid id, Guid? userId)
        {
            return id == Guid.Empty ? this.GetModelByUserId(userId) : this.GetModel(id);
        }

        public GroupWorkResult Get(Guid id)
        {
            return Db.GroupWorkResults.SingleOrDefault(e => e.TableRowGuid == id);
        }

        public JsTable<GroupWorkResult> Get(PageDetail pageDetail)
        {
            var query = Db.GroupWorkResults.AsQueryable();

            List<Expression<Func<GroupWorkResult, bool>>> filters = new List<Expression<Func<GroupWorkResult, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.GroupWorkSchedule.GroupName.GroupName1).Contains(pageDetail.Search));


            Func<GroupWorkResult, GroupWorkResult> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public JsTable<vw_GroupWorkResultList> GetList(PageDetail pageDetail, Guid subjectteacherclassroomId)
        {
            var query = (from m in Db.GroupWorkSchedules
                         join stc in Db.SubjectTeacherClassRooms on m.SubjectTeacherClassRoomGuid equals stc.TableRowGuid
                         where stc.TableRowGuid == subjectteacherclassroomId
                         select new vw_GroupWorkResultList
                         {

                         }
                             ).AsQueryable();

            List<Expression<Func<vw_GroupWorkResultList, bool>>> filters = new List<Expression<Func<vw_GroupWorkResultList, bool>>>();

            if (!string.IsNullOrEmpty(pageDetail.Search)) filters.Add(m => (m.GroupName).Contains(pageDetail.Search));


            Func<vw_GroupWorkResultList, vw_GroupWorkResultList> result = e => e;

            return GridQueryHelper.JsTable(query, pageDetail, filters, result);
        }

        public Response Validate(GroupWorkResult GroupWorkResult)
        {
            if (string.IsNullOrWhiteSpace(GroupWorkResult.GroupWorkScheduleGuid.ToString()))
                return new FailedResponse("Please provide a valid Student Name.");

            //if (string.IsNullOrWhiteSpace(GroupWorkResult.MonthGuid.ToString()))
            //    return new FailedResponse("Please provide a valid Month Name.");

            //if (string.IsNullOrWhiteSpace(GroupWorkResult.GroupWorkResultDate.ToString()))
            //    return new FailedResponse("Please provide a valid GroupWorkResult Date.");

            //if (string.IsNullOrWhiteSpace(GroupWorkResult.ReciptionNumber.ToString()))
            //    return new FailedResponse("Please provide a valid Reciption Number.");

            //if (GroupWorkResult.TableRowGuid == Guid.Empty)
            //{
            //    var queryStudent = Db.GroupWorkResults.Where(r => r.StudentGuid == GroupWorkResult.StudentGuid);
            //    var queryMonth = Db.GroupWorkResults.Where(r => r.MonthGuid == GroupWorkResult.MonthGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("A GroupWorkResult provided has already been created. Please provide a different Student or Month.");
            //}
            //else
            //{
            //    var queryStudent = Db.GroupWorkResults.Where(r => r.StudentGuid == GroupWorkResult.StudentGuid && r.TableRowGuid != GroupWorkResult.TableRowGuid);
            //    var queryMonth = Db.GroupWorkResults.Where(r => r.MonthGuid == GroupWorkResult.MonthGuid && r.TableRowGuid != GroupWorkResult.TableRowGuid);
            //    if ((queryStudent.ToList().Count > 0) && (queryMonth.ToList().Count > 0))
            //        return new FailedResponse("An GroupWorkResult provided has already been created. Please provide a different Student or Month.");
            //}

            return new SuccessResponse("");
        }

        public Response Add(GroupWorkResult GroupWorkResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(GroupWorkResult);
            if (!response.Success) return response;

            GroupWorkResult.TableRowGuid = Guid.NewGuid();
            GroupWorkResult.UserId = userid;

            return base.Add(GroupWorkResult);
        }

        public Response Update(GroupWorkResult GroupWorkResult, Guid? userid)
        {
            Response response = new Response();

            response = Validate(GroupWorkResult);
            if (!response.Success) return response;

            GroupWorkResult.UserId = userid;

            return base.Update(GroupWorkResult);
        }

        public Response Delete(GroupWorkResult GroupWorkResult, Guid? userid)
        {
            GroupWorkResult.UserId = userid;
            return base.Delete(GroupWorkResult);
        }

        public Response Delete(Guid id, Guid? userid)
        {
            return this.Delete(new GroupWorkResult() { TableRowGuid = id }, userid);
        }


    }
}
