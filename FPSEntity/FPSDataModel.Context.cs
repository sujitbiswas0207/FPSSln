﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FPSEntity.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FPSDatabaseEntities : DbContext
    {
        public FPSDatabaseEntities()
            : base("name=FPSDataModel")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        public virtual DbSet<ItemDetail> ItemDetails { get; set; }
        public virtual DbSet<QuickQuoteDetail> QuickQuoteDetails { get; set; }
    }
}
