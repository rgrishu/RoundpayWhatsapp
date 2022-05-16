using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsapp.Data;
using Whatsapp.Models.Data;
using Whatsapp.Models.ViewModel;

namespace Whatsapp.Models.Data
{
    public class ApplicationContext : IdentityDbContext<WhatsappUser, IdentityRole<int>, int>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {

        }
        public DbSet<WhatsappUser> AspNetUsers { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<MasterPackage> MasterPackage { get; set; }
        public DbSet<MasterService> MasterService { get; set; }
        public DbSet<MasterServiceFeatures> MasterServiceFeatures { get; set; }
        public DbSet<ApiRequestResponseLog> ApiRequestResponseLog { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<SenderNo> SenderNo { get; set; }
        public DbSet<SendSms> SendSms { get; set; }
        public DbSet<SendEmail> SendEmail { get; set; }
        public DbSet<MasterApi> MasterApi { get; set; }
        public DbSet<MasterApiType> MasterApiType { get; set; }
        public DbSet<EmailSetting> EmailSetting { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("blogging");
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedRoles();
            modelBuilder.SeedMasterApiType();
            //  modelBuilder.Entity<City>();

        }

    }
}
