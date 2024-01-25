using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public class ComboItem
    {
        public Guid Value { get; set; }
        public string Text { get; set; }
    }

    public class SelectItem
    {
        public string value { get; set; }
        public string label { get; set; }
    }
}
