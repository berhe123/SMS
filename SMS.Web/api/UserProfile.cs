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
    public class UserProfileController : ReadOnlyEntityController<UserProfile, UserProfile, UserProfile>
    {
        public UserProfileController(IReadOnlyEntitySvc<UserProfile, UserProfile, UserProfile> Svc, ILogger logger)
            : base(Svc, logger)
        {

        }       
        public Guid GetUserId()
        {
            return nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name);
        }
        public string GetUserFullName()
        {
            UserProfile user = (Svc as SMS.Business.Service.UserProfileSvc).Get(nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
            return user.FullName;
        }
        public int GetEventSource()
        {
            return 1;

        }
        public string GetComputerName()
        {
            return "HostName";

        }

        [HttpPost]
        public Response SaveUserRoles(UserRole UserInRoles)
        {
            return (Svc as SMS.Business.Service.UserProfileSvc).SaveUserRoles(UserInRoles);
        }
    }
}