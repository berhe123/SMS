using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class ExamSchedule
    {
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public IEnumerable<ComboItem> Teachers { get; set; }
        public IEnumerable<ComboItem> Days { get; set; }
        public IEnumerable<ComboItem> Sessions { get; set; }
        public IEnumerable<ComboItem> Examiners { get; set; }
        public Guid classroomGuid { get; set; }
    }

   
}
