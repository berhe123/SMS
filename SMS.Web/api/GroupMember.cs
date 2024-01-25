using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMS.Core;
using SMS.Entities;
using System.Web;

namespace SMS.Web.api
{
    public class GroupMemberController : EntityController<GroupMember, GroupMember, GroupMember>
    {
        protected IComboEntitySvc<GroupMember, GroupMember, GroupMember> cmbSvc;

        public GroupMemberController(IComboEntitySvc<GroupMember, GroupMember, GroupMember> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        //public IEnumerable<ComboItem> GetGroupNamesBySubjectTeacherClassRoomId(Guid id)
        //{
        //    return (Svc as SMS.Business.Service.GroupMemberSvc).GetGroupNamesBySubjectTeacherClassRoomId(id);
        //}

        //[HttpPost]
        //public JsTable<vw_GroupMemberList> GetList(PageDetail pageDetail)
        //{
        //    return (Svc as SMS.Business.Service.GroupMemberSvc).GetList(pageDetail);
        //}

        //[HttpPost]
        //public Response CreateGroupMember(StudentGroupMember StudentsInGroupMember)
        //{
        //    return (Svc as SMS.Business.Service.GroupMemberSvc).CreateGroupMember(StudentsInGroupMember);
        //}
        //public IEnumerable<ComboItem> GetGroupNamesBySubjectTeaacherClassRoomGuid(Guid id)
        //{
        //    return (Svc as SMS.Business.Service.GroupMemberSvc).GetGroupNamesBySubjectTeaacherClassRoomGuid(id);
        //}

    }
}