using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class ExamResult
    {
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public IEnumerable<ComboItem> Subjects { get; set; }
        public IEnumerable<ComboItem> ExamSchedules { get; set; }
        public Guid classroomGuid { get; set; }
    }

   
}
