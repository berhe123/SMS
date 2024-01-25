using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class StudentClassRoom
    {
        public IEnumerable<ComboItem> Students { get; set; }
        public IEnumerable<ComboItem> ClassRooms { get; set; }
    }

   
}
