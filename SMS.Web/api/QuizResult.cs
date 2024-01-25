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
    public class QuizResultController : EntityController<QuizResult, QuizResult, QuizResult>
    {
        protected IComboEntitySvc<QuizResult, QuizResult, QuizResult> cmbSvc;

        public QuizResultController(IComboEntitySvc<QuizResult, QuizResult, QuizResult> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }       

        [HttpPost]
        public Response SaveStudentResultByBatch(List<StudentQuizResult> studentQuizResult)
        {
            return (Svc as SMS.Business.Service.QuizResultSvc).SaveStudentResultByBatch(studentQuizResult);
        }
    }
}