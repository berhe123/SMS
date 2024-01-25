using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class StudentGroupMember
    {
        public Guid SubjectTeacherClassRoomGuid { get; set; }
        public Guid[] studentsGuid { get; set; }
        public string GroupName { get; set; }
        public Guid TableRowGuid { get; set; }
     }

   
}
