﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionHopital
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Gestion_Hopital1Entities : DbContext
    {
        public Gestion_Hopital1Entities()
            : base("name=Gestion_Hopital1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admission> Admissions { get; set; }
        public virtual DbSet<Affectation> Affectations { get; set; }
        public virtual DbSet<Assurance> Assurances { get; set; }
        public virtual DbSet<Departement> Departements { get; set; }
        public virtual DbSet<DossierVaccin> DossierVaccins { get; set; }
        public virtual DbSet<Infirmier> Infirmiers { get; set; }
        public virtual DbSet<Lit> Lits { get; set; }
        public virtual DbSet<Medecin> Medecins { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Prepose> Preposes { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TypeLit> TypeLits { get; set; }
        public virtual DbSet<Vaccin> Vaccins { get; set; }
    }
}
