//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmsManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfficialsPosition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OfficialsPosition()
        {
            this.OfficialsInformations = new HashSet<OfficialsInformation>();
        }
    
        public int descid { get; set; }
        public string keword { get; set; }
        public string description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfficialsInformation> OfficialsInformations { get; set; }
    }
}
