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
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.UsersInLocations = new HashSet<UsersInLocation>();
            this.security_Roles = new HashSet<security_Roles>();
        }
    
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    
        public virtual ICollection<UsersInLocation> UsersInLocations { get; set; }
        public virtual ICollection<security_Roles> security_Roles { get; set; }
    }
}
