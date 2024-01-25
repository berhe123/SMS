using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class GroupWorkSchedule
    {
        public IEnumerable<ComboItem> ClassRoomSubjects { get; set; }
        public IEnumerable<ComboItem> GroupMembers { get; set; }
        public IEnumerable<ComboItem> GroupNames { get; set; }
        public Guid subjectteacherclassroomGuid { get; set; }
        public Guid groupGuid { get; set; }
    }

   
}
