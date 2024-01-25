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

    public class EnumController : ApiController
    {
        protected readonly ILogger Logger;

        public EnumController(ILogger logger)
        {
            this.Logger = logger;
        }

        [HttpGet]
        public string GetEnumDescription(string id)
        {
            return Enums.GetEnumDescription<PaymentModeValues>(id);
        }
    }
    
}                                                                                                                             