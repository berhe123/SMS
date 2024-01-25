using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities
{
    public partial class SubjectTeacherClassRoom
    {
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public IEnumerable<ComboItem> Subjects { get; set; }
        public IEnumerable<ComboItem> Teachers { get; set; }
        public bool homeClassRoom { get; set; }
    }
}
