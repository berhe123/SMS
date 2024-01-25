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
    public class UsersInLocationController : EntityController<UsersInLocation, UsersInLocation, UsersInLocation>
    {

        public UsersInLocationController(IEntitySvc<UsersInLocation, UsersInLocation, UsersInLocation> Svc, ILogger logger)
            : base(Svc, logger)
        {
         
        }
        public Guid[] GetUserLocations(Guid id)
        {
            return (Svc as SMS.Business.Service.UsersInLocationSvc).GetUserLocations(id);
        }
        [HttpPost]
        public Response SaveUserLocations(UserLocation UserInLocations)
        {         
            UserProfileController user = new UserProfileController(null, null);      
            return (Svc as SMS.Business.Service.UsersInLocationSvc).SaveUserLocations(UserInLocations, user.GetUserId(), user.GetEventSource(), user.GetComputerName());
        }
        
    }
}                                                                                    