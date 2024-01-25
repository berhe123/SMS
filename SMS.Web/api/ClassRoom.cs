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
    public class ClassRoomController : EntityController<ClassRoom, ClassRoom, ClassRoom>
    {
        protected IComboEntitySvc<ClassRoom, ClassRoom, ClassRoom> cmbSvc;

        public ClassRoomController(IComboEntitySvc<ClassRoom, ClassRoom, ClassRoom> Svc, ILogger logger)
            : base(Svc, logger)
        {
            cmbSvc = Svc;
        }

        public IEnumerable<ComboItem> GetComboItems()
        {
            return cmbSvc.GetComboItems();
        }
    }
}