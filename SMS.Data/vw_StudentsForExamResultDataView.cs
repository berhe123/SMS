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
    
    public partial class vw_StudentsForExamResultDataView
    {
        public string Exam { get; set; }
        public Nullable<int> OutOf { get; set; }
        public decimal Result { get; set; }
        public string Comment { get; set; }
        public string StudentFullName { get; set; }
        public string TeacherFullName { get; set; }
        public System.Guid StudentGuid { get; set; }
        public Nullable<System.Guid> ClassroomGuid { get; set; }
        public Nullable<System.Guid> SubjectGuid { get; set; }
        public Nullable<System.Guid> TeacherGuid { get; set; }
        public Nullable<System.Guid> SubjectTeacherClassRoomGuid { get; set; }
        public System.Guid ExamScheduleGuid { get; set; }
        public System.Guid ExamResultGuid { get; set; }
    }
}