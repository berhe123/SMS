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
    public class FilterSpecController : ApiController
    {
        protected readonly ILogger Logger;

        public FilterSpecController(ILogger logger)
        {
            this.Logger = logger;
        }

        [HttpGet]
        public FilterSpec GetFilterSpec()
        {
            return (new SMS.Business.Service.FilterSpecSvc(this.Logger)).GetFilterSpec(nonintanon.Security.WebSecurity.GetUserId(User.Identity.Name));
        }
    }
}                                                                                                                             