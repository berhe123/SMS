using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Entities
{
    public partial class QuizResult
    {
        public IEnumerable<ComboItem> Quizs { get; set; }
        public IEnumerable<ComboItem> ClassRooms { get; set; }
        public Guid classroomGuid { get; set; }
    }

   
}
