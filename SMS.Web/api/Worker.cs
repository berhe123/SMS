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
    public class WorkerController : EntityController<Worker, Worker, Worker>
    {
        protected IComboEntitySvc<Worker, Worker, Worker> cmbSvc;

        public WorkerController(IComboEntitySvc<Worker, Worker, Worker> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }

        [HttpPost]
        public JsTable<vw_WorkerList> GetList(PageDetail pageDetail)
        {
            return (Svc as SMS.Business.Service.WorkerSvc).GetList(pageDetail);
        }      
    }
}