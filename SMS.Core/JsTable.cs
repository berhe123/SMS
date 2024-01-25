using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Core
{
    public class JsTable<T>
    {
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public List<T> aaData { get; set; }
    }
}
