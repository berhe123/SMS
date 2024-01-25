using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public class UserLocation
    {
        public Guid UserId { get; set; }
        public Guid[] SelectedUserLocations { get; set; }
    }
}
