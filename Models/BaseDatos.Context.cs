﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inluirme_Proyecto.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PROYECTO_INCLUIREntities : DbContext
    {
        public PROYECTO_INCLUIREntities()
            : base("name=PROYECTO_INCLUIREntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Capacitaciones> Capacitaciones { get; set; }
        public virtual DbSet<Empresas> Empresas { get; set; }
        public virtual DbSet<Encuestas> Encuestas { get; set; }
        public virtual DbSet<Preguntas> Preguntas { get; set; }
        public virtual DbSet<teaCapacitar> teaCapacitar { get; set; }
        public virtual DbSet<Universidades> Universidades { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
