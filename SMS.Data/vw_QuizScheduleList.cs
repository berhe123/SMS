//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMS.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_QuizScheduleList
    {
        public string Quiz { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string QuizName { get; set; }
        public int OutOf { get; set; }
        public string QuizInformation { get; set; }
        public System.DateTime GivenDate { get; set; }
        public System.Guid QuizScheduleGuid { get; set; }
        public System.Guid LessonClassGuid { get; set; }
        public System.Guid SubjectTeacherClassRoomGuid { get; set; }
        public System.Guid ClassRoomGuid { get; set; }
        public System.Guid SubjectGuid { get; set; }
        public System.Guid TeacherGuid { get; set; }
        public System.Guid DayGuid { get; set; }
        public System.Guid PeriodGuid { get; set; }
        public System.Guid SessionGuid { get; set; }
    }
}
