﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AstanaCheTamBrat.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ACTBEntities : DbContext
    {
        public ACTBEntities()
            : base("name=ACTBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<pages> pages { get; set; }
        public virtual DbSet<products> products { get; set; }
        public virtual DbSet<products_categories> products_categories { get; set; }
        public virtual DbSet<provider_requests> provider_requests { get; set; }
        public virtual DbSet<providers> providers { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<providers_temp> providers_temp { get; set; }
        public virtual DbSet<products_temp> products_temp { get; set; }
    }
}
