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
    public class StudentClassRoomController : EntityController<StudentClassRoom, StudentClassRoom, StudentClassRoom>
    {
        protected IComboEntitySvc<StudentClassRoom, StudentClassRoom, StudentClassRoom> cmbSvc;

        public StudentClassRoomController(IComboEntitySvc<StudentClassRoom, StudentClassRoom, StudentClassRoom> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_StudentClassRoomList> GetList(PageDetail pageDetail)
        {
            return (Svc as SMS.Business.Service.StudentClassRoomSvc).GetList(pageDetail);
        }           

    }
}