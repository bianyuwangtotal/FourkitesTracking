﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrackingAssistance.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class SqlSrv01Entities : DbContext
    {
        public SqlSrv01Entities()
            : base("name=SqlSrv01Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<backupLoad> backupLoads { get; set; }
        public virtual DbSet<FourKites_CustomerLane> FourKites_CustomerLane { get; set; }
        public virtual DbSet<FourKites_GPS_Sequencer> FourKites_GPS_Sequencer { get; set; }
        public virtual DbSet<FourKitesTrackingPendingLeg> FourKitesTrackingPendingLegs { get; set; }
        public virtual DbSet<FourkitesTrackingSentLeg> FourkitesTrackingSentLegs { get; set; }
        public virtual DbSet<TrackingAssistanceUser> TrackingAssistanceUsers { get; set; }
        public virtual DbSet<TrackingBackup> TrackingBackups { get; set; }

        internal List<LoadDisplay> getLoadInfo(string bol)
        {
            throw new NotImplementedException();
        }
    }
}
