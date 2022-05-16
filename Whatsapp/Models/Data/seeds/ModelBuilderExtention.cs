using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Whatsapp.Models;

namespace Whatsapp.Data
{
    public static class ModelBuilderExtention
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                    new IdentityRole<int>() { Id = 1, ConcurrencyStamp = "d56d9543-b61e-4c7a-a611-ebc073d5da71", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole<int>() { Id = 2, ConcurrencyStamp = "23e0f213-c8fa-4d53-bf9b-5c6220d2e3ba", Name = "Seller", NormalizedName = "SELLER" },
                    new IdentityRole<int>() { Id = 3, ConcurrencyStamp = "da576a70-2276-4872-a496-6765a07534e6", Name = "ReSeller", NormalizedName = "RESELLER" },
                    new IdentityRole<int>() { Id = 4, ConcurrencyStamp = "b52abe85-ca0e-44ae-a4d0-fd1b315576ee", Name = "WhiteLabel", NormalizedName = "WHITELEABEL" }
               );
        }

        public static void SeedMasterApiType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasterApiType>().HasData(
                    new MasterApiType() {  ApiTypeName = "Whatsapp"},
                    new MasterApiType() { ApiTypeName = "Email"},
                    new MasterApiType() { ApiTypeName = "SMS"  }
               );
        }
    }
}
