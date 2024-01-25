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
    public class AcademicCalanderController : EntityController<AcademicCalander, AcademicCalander, AcademicCalander>
    {
        protected IComboEntitySvc<AcademicCalander, AcademicCalander, AcademicCalander> cmbSvc;

        public AcademicCalanderController(IComboEntitySvc<AcademicCalander, AcademicCalander, AcademicCalander> Svc, ILogger logger)
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