using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class Salary
    {
        public IEnumerable<ComboItem> Teachers { get; set; }
        public IEnumerable<ComboItem> Months { get; set; }
    }

   
}
