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
    
    public partial class ParentComment
    {
        public System.Guid ParentGuid { get; set; }
        public string Comment { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<int> EventSource { get; set; }
        public string ComputerName { get; set; }
        public System.Guid TableRowGuid { get; set; }
    
        public virtual Parent Parent { get; set; }
    }
}
