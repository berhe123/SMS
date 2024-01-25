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
    public class RoleController : ReadOnlyEntityController<security_Roles, security_Roles, security_Roles>
    {
        public RoleController(IReadOnlyEntitySvc<security_Roles, security_Roles, security_Roles> Svc, ILogger logger)
            : base(Svc, logger)
        {

        }      
        public Guid[] GetUserRoles(Guid id)
        {
            return (Svc as SMS.Business.Service.RoleSvc).GetUserRoles(id);
        }
       
       
    }
}