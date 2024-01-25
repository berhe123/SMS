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
    public class ExamResultController : EntityController<ExamResult, ExamResult, ExamResult>
    {
        protected IComboEntitySvc<ExamResult, ExamResult, ExamResult> cmbSvc;

        public ExamResultController(IComboEntitySvc<ExamResult, ExamResult, ExamResult> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }       

        [HttpPost]
        public Response SaveStudentResultByBatch(List<StudentExamResult> studentExamResult)
        {
            return (Svc as SMS.Business.Service.ExamResultSvc).SaveStudentResultByBatch(studentExamResult);
        }

    }
}