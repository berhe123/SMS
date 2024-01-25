using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class Payment
    {
        public IEnumerable<ComboItem> Students { get; set; }
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public IEnumerable<ComboItem> PaymentCalanders { get; set; }
        public IEnumerable<ComboItem> FeeSettings { get; set; }
        public Guid classroomGuid { get; set; }
        public Guid paymentcalanderGuid { get; set; }
    }

   
}
