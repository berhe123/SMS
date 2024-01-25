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
    public class ExerciseBookResultController : EntityController<ExerciseBookResult, ExerciseBookResult, ExerciseBookResult>
    {
        protected IComboEntitySvc<ExerciseBookResult, ExerciseBookResult, ExerciseBookResult> cmbSvc;

        public ExerciseBookResultController(IComboEntitySvc<ExerciseBookResult, ExerciseBookResult, ExerciseBookResult> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public Response SaveStudentResultByBatch(List<StudentExerciseBookResult> studentExerciseBookResult)
        {
            return (Svc as SMS.Business.Service.ExerciseBookResultSvc).SaveStudentResultByBatch(studentExerciseBookResult);
        }

    }
}