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
    public class StudentCommentController : EntityController<StudentComment, StudentComment, StudentComment>
    {
        protected IComboEntitySvc<StudentComment, StudentComment, StudentComment> cmbSvc;

        public StudentCommentController(IComboEntitySvc<StudentComment, StudentComment, StudentComment> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }       

    }
}