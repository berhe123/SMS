using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class ExerciseBookResult
    {
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public IEnumerable<ComboItem> Subjects { get; set; }
        public IEnumerable<ComboItem> ClassRoomSubjects { get; set; }
        public Guid classroomGuid { get; set; }
    }

   
}
