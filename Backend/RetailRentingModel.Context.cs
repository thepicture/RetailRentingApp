﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RetailRentingApp.Backend
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RetailRentingBaseEntities : DbContext
    {
        public RetailRentingBaseEntities()
            : base("name=RetailRentingBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cheque> Cheques { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PhoneNumberOfCustomer> PhoneNumberOfCustomers { get; set; }
        public virtual DbSet<Renting> Rentings { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TradingArea> TradingAreas { get; set; }
        public virtual DbSet<TypeOfUser> TypeOfUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}