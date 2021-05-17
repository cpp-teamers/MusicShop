using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using ModelsLibrary.Models;

namespace ModelsLibrary.EF
{
    public partial class ModelsManager : DbContext
    {
        public ModelsManager() : base("name=ModelsManager") { }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Plate> Plates { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
