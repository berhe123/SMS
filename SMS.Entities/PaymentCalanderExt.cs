﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities
{
    public partial class PaymentCalander
    {
        public IEnumerable<ComboItem> Months { get; set; }
    }
}