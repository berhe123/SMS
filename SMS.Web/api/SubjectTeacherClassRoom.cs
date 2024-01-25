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
    public class SubjectTeacherClassRoomController : EntityController<SubjectTeacherClassRoom, SubjectTeacherClassRoom, SubjectTeacherClassRoom>
    {
        protected IComboEntitySvc<SubjectTeacherClassRoom, SubjectTeacherClassRoom, SubjectTeacherClassRoom> cmbSvc;

        public SubjectTeacherClassRoomController(IComboEntitySvc<SubjectTeacherClassRoom, SubjectTeacherClassRoom, SubjectTeacherClassRoom> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }
        [HttpPost]
        public JsTable<vw_SubjectTeacherClassRoomList> GetList(PageDetail pageDetail)
        {           
            return (Svc as SMS.Business.Service.SubjectTeacherClassRoomSvc).GetList(pageDetail);
        }
        public IEnumerable<ComboItem> GetClassRoomsByTeacherUserId(Guid userId)
        {
            return (Svc as SMS.Business.Service.SubjectTeacherClassRoomSvc).GetClassRoomsByTeacherGuid(userId);
        }
        public IEnumerable<ComboItem> GetClassRoomSubjectsByTeacherGuid(Guid id)
        {
            return (Svc as SMS.Business.Service.SubjectTeacherClassRoomSvc).GetClassRoomSubjectsByTeacherGuid(id);
        }
        public IEnumerable<ComboItem> GetSubjectsByClassRoomId(Guid id)
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.SubjectTeacherClassRoomSvc).GetSubjectsByClassRoomId(userId, id);
        }
        public IEnumerable<ComboItem> GetTeacherByClassRoomGuid(Guid id)
        {
            return (Svc as SMS.Business.Service.SubjectTeacherClassRoomSvc).GetTeacherByClassRoomGuid(id);
        }
        public IEnumerable<ComboItem> GetClassRoomSubjectByTeacherGuid()
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.SubjectTeacherClassRoomSvc).GetClassRoomSubjectByTeacherGuid(userId);
        }
        public IEnumerable<ComboItem> GetClassRoomSubjectsByTeacherUserId()
        {
            Guid userId = nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
            return (Svc as SMS.Business.Service.SubjectTeacherClassRoomSvc).GetClassRoomSubjectByTeacherGuid(userId);
        }
        }
    }