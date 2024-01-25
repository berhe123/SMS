using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public class TeacherSubjectClass
    {
        public Guid TeacherTableRowGuid { get; set; }
        public Guid[] SubjectTableRowGuid { get; set; }

        public Guid[] ClassRoomTableRowGuid { get; set; }
    }
}
