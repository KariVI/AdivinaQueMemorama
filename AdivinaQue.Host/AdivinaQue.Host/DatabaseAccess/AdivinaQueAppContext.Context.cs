﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdivinaQue.Host.DatabaseAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AdivinaQueAppContext : DbContext
    {
        public AdivinaQueAppContext()
            : base("name=AdivinaQueAppContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Participate> Participate { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Score> Score { get; set; }
    }
}