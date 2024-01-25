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
    public class IndividualWorkResultController : EntityController<IndividualWorkResult, IndividualWorkResult, IndividualWorkResult>
    {
        protected IComboEntitySvc<IndividualWorkResult, IndividualWorkResult, IndividualWorkResult> cmbSvc;

        public IndividualWorkResultController(IComboEntitySvc<IndividualWorkResult, IndividualWorkResult, IndividualWorkResult> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }
        
        [HttpPost]
        public Response SaveStudentResultByBatch(List<StudentIndividualWorkResult> studentAssigmentResult)
        {
            return (Svc as SMS.Business.Service.IndividualWorkResultSvc).SaveStudentResultByBatch(studentAssigmentResult);

        }


    }
}