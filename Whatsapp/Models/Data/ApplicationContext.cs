using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsapp.Models.Data;
using Whatsapp.Models.ViewModel;

namespace Whatsapp.Models.Data
{
    public class ApplicationContext : IdentityDbContext<WhatsappUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {

        }
        public DbSet<State> State { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<test1> test1 { get; set; }
        public DbSet<MasterPackage> MasterPackage { get; set; }
        public DbSet<MasterService> MasterService { get; set; }
        public DbSet<MasterServiceFeatures> MasterServiceFeatures { get; set; }
        public DbSet<Package> Package { get; set; }
        //public virtual ICollection<State> State { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.HasDefaultSchema("blogging");
             base.OnModelCreating(modelBuilder);
            //  modelBuilder.Entity<City>();

        }
      
    }
}
