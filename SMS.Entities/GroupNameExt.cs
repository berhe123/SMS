
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class GroupName
    {
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public IEnumerable<ComboItem> Subjects { get; set; }
        public Guid subjectteacherclassroomGuid { get; set; }
        public Guid ClassRoomGuid { get; set; }

        //public IEnumerable<ComboItem> ClassRoomSubjects { get; set; }
        //public IEnumerable<ComboItem> GroupNames { get; set; }
     }

   
}
