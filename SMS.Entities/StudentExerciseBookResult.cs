using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities
{
    public class StudentExerciseBookResult
    {

        public Guid? TableRowGuid { get; set; }
        public Guid StudentGuid { get; set; }
        public Guid SubjectTeacherClassRoomGuid { get; set; }
        public string OutOf { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
    }
}
