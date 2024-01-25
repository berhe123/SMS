using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class Teacher
    {
        public IEnumerable<ComboItem> Genders { get; set; }
        public IEnumerable<ComboItem> Users { get; set; }
    }

   
}
