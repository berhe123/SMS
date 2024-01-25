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
    public class TestResultController : EntityController<TestResult, TestResult, TestResult>
    {
        protected IComboEntitySvc<TestResult, TestResult, TestResult> cmbSvc;

        public TestResultController(IComboEntitySvc<TestResult, TestResult, TestResult> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }      

        [HttpPost]
        public Response SaveStudentResultByBatch(List<StudentTestResult> studentTestResult)
        {
            return (Svc as SMS.Business.Service.TestResultSvc).SaveStudentResultByBatch(studentTestResult);
        }

    }
}