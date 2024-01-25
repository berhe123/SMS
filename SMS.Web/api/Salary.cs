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
    public class SalaryController : EntityController<Salary, Salary, Salary>
    {
        protected IComboEntitySvc<Salary, Salary, Salary> cmbSvc;

        public SalaryController(IComboEntitySvc<Salary, Salary, Salary> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_SalaryList> GetList(PageDetail pageDetail)
        {           
            return (Svc as SMS.Business.Service.SalarySvc).GetList(pageDetail);
        }


    }
}