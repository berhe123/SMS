using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class IndividualWorkSchedule
    {
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public Guid IndividualWorkScheduleGuid { get; set; }
        public Guid userId { get; set; }
    }


}
