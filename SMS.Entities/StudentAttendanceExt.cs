using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class StudentAttendance
    {
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public IEnumerable<ComboItem> LessonClasses { get; set; }
        public IEnumerable<ComboItem> ClassRoomSubjects { get; set; }
    }

   
}
