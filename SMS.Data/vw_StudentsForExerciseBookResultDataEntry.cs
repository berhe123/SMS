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
    
    public partial class vw_StudentsForExerciseBookResultDataEntry
    {
        public string StudentFullName { get; set; }
        public Nullable<int> OutOf { get; set; }
        public Nullable<decimal> Result { get; set; }
        public string Comment { get; set; }
        public string TeacherFullName { get; set; }
        public string ClassRoomName { get; set; }
        public string SubjectName { get; set; }
        public Nullable<bool> TeacherHomeClassRoom { get; set; }
        public Nullable<System.Guid> StudentGuid { get; set; }
        public Nullable<System.Guid> ExerciseBookResultGuid { get; set; }
        public Nullable<System.Guid> SubjectTeacherClassRoomGuid { get; set; }
        public Nullable<System.Guid> TeacherGuid { get; set; }
        public Nullable<System.Guid> ClassRoomGuid { get; set; }
        public Nullable<System.Guid> SubjectGuid { get; set; }
    }
}