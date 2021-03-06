﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FPS.DataModel
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
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<ItemDetail> ItemDetails { get; set; }
        public virtual DbSet<QuickQuoteDetail> QuickQuoteDetails { get; set; }
        public virtual DbSet<CarriersDetail> CarriersDetails { get; set; }
        public virtual DbSet<ItemCollectionAddress> ItemCollectionAddresses { get; set; }
        public virtual DbSet<ItemDeliveryAddress> ItemDeliveryAddresses { get; set; }
        public virtual DbSet<ItemDropoffDetail> ItemDropoffDetails { get; set; }
        public virtual DbSet<ItemPickupDetail> ItemPickupDetails { get; set; }
        public virtual DbSet<ShipmentDetail> ShipmentDetails { get; set; }
        public virtual DbSet<PaymentsDetail> PaymentsDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<tblCommodityCategory> tblCommodityCategories { get; set; }
        public virtual DbSet<tblCondition> tblConditions { get; set; }
        public virtual DbSet<tblFreightClass> tblFreightClasses { get; set; }
        public virtual DbSet<tblPackaging> tblPackagings { get; set; }
        public virtual DbSet<TruckloadQuoteDetail> TruckloadQuoteDetails { get; set; }
    }
}
