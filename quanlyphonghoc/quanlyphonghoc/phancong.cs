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
    
    public partial class phancong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public phancong()
        {
            this.tkbs = new HashSet<tkb>();
            this.buoihocs = new HashSet<buoihoc>();
        }
    
        public string mapc { get; set; }
        public string magv { get; set; }
        public string mamh { get; set; }
        public string malop { get; set; }
        public int hocky { get; set; }
        public Nullable<int> namhoc { get; set; }
    
        public virtual giangvien giangvien { get; set; }
        public virtual lop lop { get; set; }
        public virtual lop lop1 { get; set; }
        public virtual monhoc monhoc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tkb> tkbs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<buoihoc> buoihocs { get; set; }
    }
}