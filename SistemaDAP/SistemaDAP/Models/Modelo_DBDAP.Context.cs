﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SistemaDAP.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBDAPEntities : DbContext
    {
        public DBDAPEntities()
            : base("name=DBDAPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<pais> pais { get; set; }
        public virtual DbSet<cargo> cargo { get; set; }
        public virtual DbSet<tipo_servicio> tipo_servicio { get; set; }
        public virtual DbSet<banco> banco { get; set; }
        public virtual DbSet<tipo_articulo> tipo_articulo { get; set; }
        public virtual DbSet<tipo_herramienta> tipo_herramienta { get; set; }
    }
}
