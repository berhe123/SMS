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
    public class EntityController<T, TD, TR> : ApiController where T : class
    {
        protected IEntitySvc<T, TD, TR> Svc;
        protected readonly ILogger Logger;
        protected string EntityName { get; set; }

        public EntityController(IEntitySvc<T, TD, TR> Svc, ILogger logger)
        {
            this.Svc = Svc;
            this.Logger = logger;
        }

        public IQueryable<TR> Get()
        {
            return Svc.Get();
        }

        public T Get(Guid id)
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
            return Svc.GetModel(Guid.Empty, nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
            //return Svc.GetModel();
        }

        [HttpGet]
        public T GetModel(Guid id)
        {
            return Svc.GetModel(id, nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
            //return Svc.GetModel(id);
        }

        [HttpPost]
        public Response Add(T t)
        {
            System.Reflection.PropertyInfo computerName = t.GetType().GetProperty("ComputerName");
            System.Reflection.PropertyInfo eventSource = t.GetType().GetProperty("EventSource");

            if (computerName != null) t.GetType().InvokeMember(computerName.Name, System.Reflection.BindingFlags.SetProperty, null, t, new object[] { "ComputerName" });
            if (eventSource != null) t.GetType().InvokeMember(eventSource.Name, System.Reflection.BindingFlags.SetProperty, null, t, new object[] { 1 });

            return Svc.Add(t, nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
        }

        [HttpPost]
        public Response Update(T t)
        {
            System.Reflection.PropertyInfo computerName = t.GetType().GetProperty("ComputerName");
            System.Reflection.PropertyInfo eventSource = t.GetType().GetProperty("EventSource");
             
            if (computerName != null) t.GetType().InvokeMember(computerName.Name, System.Reflection.BindingFlags.SetProperty, null, t, new object[] { "ComputerName" });
            if (eventSource != null) t.GetType().InvokeMember(eventSource.Name, System.Reflection.BindingFlags.SetProperty, null, t, new object[] { 1 });

            return Svc.Update(t, nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
        }

        [HttpGet]
        public Response Delete(Guid id)
        {
            return Svc.Delete(id, nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
        }
    }
}