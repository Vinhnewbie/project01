﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class quanlylichhoc2Entities : DbContext
    {
        public quanlylichhoc2Entities()
            : base("name=quanlylichhoc2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<buoihoc> buoihocs { get; set; }
        public virtual DbSet<giangvien> giangviens { get; set; }
        public virtual DbSet<lop> lops { get; set; }
        public virtual DbSet<monhoc> monhocs { get; set; }
        public virtual DbSet<nganhhoc> nganhhocs { get; set; }
        public virtual DbSet<nguoidung> nguoidungs { get; set; }
        public virtual DbSet<phancong> phancongs { get; set; }
        public virtual DbSet<phonghoc> phonghocs { get; set; }
        public virtual DbSet<sinhvien> sinhviens { get; set; }
        public virtual DbSet<tkb> tkbs { get; set; }
        public virtual DbSet<trinhdo> trinhdoes { get; set; }
    }
}
