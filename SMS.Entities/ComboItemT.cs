using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public class ComboItem<T>
    {
        public T Value { get; set; }
        public string Text { get; set; }
    }

    public class ComboItem<T, Tx>
    {
        public T Value { get; set; }
        public Tx Text { get; set; }
    }
}
