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
    
    public partial class vw_IndividualWorkScheduleList
    {
        public string IndividualWork { get; set; }
        public string IndividualWorkName { get; set; }
        public System.DateTime IndividualWorkGivenDate { get; set; }
        public System.DateTime IndividualWorkSubmissionDate { get; set; }
        public int OutOf { get; set; }
        public string IndividualWorkInformation { get; set; }
        public System.Guid SubjectGuid { get; set; }
        public System.Guid ClassRoomGuid { get; set; }
        public string TeacherFullName { get; set; }
        public System.Guid TeacherGuid { get; set; }
        public System.Guid SubjectTeacherClassRoomGuid { get; set; }
        public System.Guid IndividualWorkScheduleGuid { get; set; }
    }
}