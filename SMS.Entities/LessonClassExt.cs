using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class LessonClass
    {
        public IEnumerable<ComboItem> Days { get; set; }
        public IEnumerable<ComboItem> Periods { get; set; }
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public IEnumerable<ComboItem> Teachers { get; set; }
        public Guid TeacherGuid { get;  set; }     
    }

   
}
