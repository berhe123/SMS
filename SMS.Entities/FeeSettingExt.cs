using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class FeeSetting
    {
        public IEnumerable<ComboItem> Months { get; set; }
        public IEnumerable<ComboItem> ClassRooms { get; set; }
    }

   
}
