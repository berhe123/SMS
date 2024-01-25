using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class Student
    {
        public IEnumerable<ComboItem> Genders { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

    }

   
}
