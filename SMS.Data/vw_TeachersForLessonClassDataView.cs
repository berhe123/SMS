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
    
    public partial class vw_TeachersForLessonClassDataView
    {
        public string lesson { get; set; }
        public System.DateTime TimeFrom { get; set; }
        public System.DateTime TimeTo { get; set; }
        public string TeacherName { get; set; }
        public System.Guid lessonclassGuid { get; set; }
        public Nullable<System.Guid> subjectteacherclassroomGuid { get; set; }
        public Nullable<System.Guid> TeacherGuid { get; set; }
        public Nullable<System.Guid> ClassRoomGuid { get; set; }
        public Nullable<System.Guid> SubjectGuid { get; set; }
        public System.Guid dayGuid { get; set; }
        public System.Guid periodGuid { get; set; }
        public System.Guid sessionGuid { get; set; }
    }
}
