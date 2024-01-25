using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class TestResult
    {
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public IEnumerable<ComboItem> Tests { get; set; }
        public Guid classroomGuid { get; set; }
    }

   
}
