//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace quanlyphonghoc
{
    using System;
    using System.Collections.Generic;
    
    public partial class monhoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public monhoc()
        {
            this.phancongs = new HashSet<phancong>();
        }
    
        public string mamh { get; set; }
        public string tenmh { get; set; }
        public Nullable<int> stc { get; set; }
        public Nullable<int> lythuyet { get; set; }
        public Nullable<int> thuchanh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<phancong> phancongs { get; set; }
    }
}
