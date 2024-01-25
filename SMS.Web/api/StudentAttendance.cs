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
    public class StudentAttendanceController : EntityController<StudentAttendance, StudentAttendance, StudentAttendance>
    {
        protected IComboEntitySvc<StudentAttendance, StudentAttendance, StudentAttendance> cmbSvc;

        public StudentAttendanceController(IComboEntitySvc<StudentAttendance, StudentAttendance, StudentAttendance> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_StudentAttendanceList> GetList(PageDetail pageDetail, Guid lessonclassId)
        {
            return (Svc as SMS.Business.Service.StudentAttendanceSvc).GetList(pageDetail, lessonclassId);
        }


    }
}