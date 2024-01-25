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
    
    public partial class vw_StudentsForQuizResultDataEntry
    {
        public string StudentFullName { get; set; }
        public Nullable<decimal> Result { get; set; }
        public string Comment { get; set; }
        public string QuizName { get; set; }
        public Nullable<int> OutOf { get; set; }
        public string QuizInformation { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> GivenDate { get; set; }
        public string TeacherFullName { get; set; }
        public string ClassRoomName { get; set; }
        public string SubjectName { get; set; }
        public Nullable<bool> HomeClassRoom { get; set; }
        public string DayName { get; set; }
        public string SessionName { get; set; }
        public string PeriodName { get; set; }
        public Nullable<System.DateTime> TimeFrom { get; set; }
        public Nullable<System.DateTime> TimeTo { get; set; }
        public Nullable<System.Guid> StudentGuid { get; set; }
        public Nullable<System.Guid> QuizResultGuid { get; set; }
        public Nullable<System.Guid> QuizScheduleGuid { get; set; }
        public Nullable<System.Guid> LessonClassGuid { get; set; }
        public Nullable<System.Guid> SubjectTeacherClassRoomGuid { get; set; }
        public Nullable<System.Guid> DayGuid { get; set; }
        public Nullable<System.Guid> PeriodGuid { get; set; }
        public Nullable<System.Guid> TeacherGuid { get; set; }
        public Nullable<System.Guid> ClassRoomGuid { get; set; }
        public Nullable<System.Guid> SubjectGuid { get; set; }
        public Nullable<System.Guid> SessionGuid { get; set; }
    }
}