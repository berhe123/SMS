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
    public class GroupNameController : EntityController<GroupName, GroupName, GroupName>
    {
        protected IComboEntitySvc<GroupName, GroupName, GroupName> cmbSvc;

        public GroupNameController(IComboEntitySvc<GroupName, GroupName, GroupName> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        public IEnumerable<ComboItem> GetGroupNamesBySubjectTeacherClassRoomId(Guid id)
        {
            return (Svc as SMS.Business.Service.GroupNameSvc).GetGroupNamesBySubjectTeacherClassRoomId(id);
        }

        [HttpPost]
        public JsTable<vw_GroupNameList> GetList(PageDetail pageDetail)
        {
            return (Svc as SMS.Business.Service.GroupNameSvc).GetList(pageDetail);
        }
        
        public IEnumerable<ComboItem> GetGroupNamesBySubjectTeaacherClassRoomGuid(Guid id)
        {
            return (Svc as SMS.Business.Service.GroupNameSvc).GetGroupNamesBySubjectTeaacherClassRoomGuid(id);
        }

        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid id)
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.GroupNameSvc).GetSubjectsByClassRoomId(userId, id);
        }       
    }
}