using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class IndividualWorkResult
    {
        public IEnumerable<ComboItem> Students { get; set; }
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public IEnumerable<ComboItem> IndividualWorkSchedules { get; set; }
        public IEnumerable<ComboItem> ClassRoomSubjects { get; set; }
        public Guid ClassRoomGuid { get; set; }
        public Guid SubjectTeacherClassRoom { get; set; }

    }

   
}
