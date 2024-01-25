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
    
    public partial class GroupWorkSchedule
    {
        public GroupWorkSchedule()
        {
            this.GroupWorkResults = new HashSet<GroupWorkResult>();
        }
    
        public System.Guid SubjectTeacherClassRoomGuid { get; set; }
        public string GroupWorkTitle { get; set; }
        public System.Guid GroupNameGuid { get; set; }
        public System.DateTime GroupWorkGivenDate { get; set; }
        public System.DateTime GroupWorkSubmissionDate { get; set; }
        public int OutOf { get; set; }
        public string Information { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<int> EventSource { get; set; }
        public string ComputerName { get; set; }
        public System.Guid TableRowGuid { get; set; }
    
        public virtual GroupName GroupName { get; set; }
        public virtual ICollection<GroupWorkResult> GroupWorkResults { get; set; }
        public virtual SubjectTeacherClassRoom SubjectTeacherClassRoom { get; set; }
    }
}