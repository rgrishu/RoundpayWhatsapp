using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsapp.Models.Data;

namespace Whatsapp.Models.Data
{
    public class ApplicationContext : IdentityDbContext<WhatsappUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {

        }
        public DbSet<State> State { get; set; }
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
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.HasDefaultSchema("dbo");
        //    builder.Entity<IdentityUser>(entity =>
        //    {
        //        entity.ToTable(name: "User");
        //    });
        //    //builder.Entity<IdentityRole>(entity =>
        //    //{
        //    //    entity.ToTable(name: "Role");
        //    //});
        //    //builder.Entity<IdentityUserRole<string>>(entity =>
        //    //{
        //    //    entity.ToTable("UserRoles");
        //    //});
        //    //builder.Entity<IdentityUserClaim<string>>(entity =>
        //    //{
        //    //    entity.ToTable("UserClaims");
        //    //});
        //    //builder.Entity<IdentityUserLogin<string>>(entity =>
        //    //{
        //    //    entity.ToTable("UserLogins");
        //    //});
        //    //builder.Entity<IdentityRoleClaim<string>>(entity =>
        //    //{
        //    //    entity.ToTable("RoleClaims");
        //    //});
        //    //builder.Entity<IdentityUserToken<string>>(entity =>
        //    //{
        //    //    entity.ToTable("UserTokens");
        //    //});
        //}
    }
}
