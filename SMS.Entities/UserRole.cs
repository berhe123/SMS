using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public class  UserRole
    {
        public Guid UserId { get; set; }
        public Guid[] SelectedUserRoles { get; set; }
    }
}
