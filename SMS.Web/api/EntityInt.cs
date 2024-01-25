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
    public class EntityIntController<T, TD, TR> : ApiController where T : class
    {
        protected IEntityIntSvc<T, TD, TR> Svc;
        protected readonly ILogger Logger;
        protected string EntityName { get; set; }

        public EntityIntController(IEntityIntSvc<T, TD, TR> Svc, ILogger logger)
        {
            this.Svc = Svc;
            this.Logger = logger;
        }

        public IQueryable<TR> Get()
        {
            return Svc.Get();
        }

        public T Get(long id)
        {
            return Svc.Get(id);
        }

        [HttpPost]
        public JsTable<TD> Get(PageDetail pageDetail)
        {
            return Svc.Get(pageDetail);
        }


        [HttpGet]
        public T GetModel()
        {
            return Svc.GetModel();
        }

        [HttpGet]
        public T GetModel(long id)
        {
            return Svc.GetModel(id);
        }

        [HttpPost]
        public Response Add(T t)
        {
            return Svc.Add(t, nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
        }

        [HttpPost]
        public Response Update(T t)
        {
            return Svc.Update(t, nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
        }

        [HttpGet]
        public Response Delete(long id)
        {
            return Svc.Delete(id, nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
        }
    }
}