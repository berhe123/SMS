﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class QuizSchedule
    {
        private string starttime;
        public IEnumerable<ComboItem> ClassRoomSubjects { get; set; }
        public IEnumerable<ComboItem> Periods { get; set; }
        public Guid classroomsubjectGuid { get; set; }       
    }

   
}