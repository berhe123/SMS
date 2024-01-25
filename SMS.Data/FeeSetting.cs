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
    
    public partial class FeeSetting
    {
        public FeeSetting()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public System.Guid MonthGuid { get; set; }
        public System.Guid ClassRoomGuid { get; set; }
        public decimal Amount { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<int> EventSource { get; set; }
        public string ComputerName { get; set; }
        public System.Guid TableRowGuid { get; set; }
    
        public virtual ClassRoom ClassRoom { get; set; }
        public virtual Month Month { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
