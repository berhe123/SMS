﻿using System;
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
    public class UsersInRoleController : EntityController<security_Roles, security_Roles, security_Roles>
    {

        public UsersInRoleController(IEntitySvc<security_Roles, security_Roles, security_Roles> Svc, ILogger logger)
            : base(Svc, logger)
        {
           
        }
   
        
    }
}                                                                                    