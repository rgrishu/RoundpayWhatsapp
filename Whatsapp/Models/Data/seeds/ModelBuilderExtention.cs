using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Whatsapp.Models;
using Whatsapp.Models.Data;

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
                    new MasterApiType() { Id = 1, ApiTypeName = "Whatsapp" },
                    new MasterApiType() { Id = 2, ApiTypeName = "Email" },
                    new MasterApiType() { Id = 3, ApiTypeName = "SMS" }
               );
        }
        public static void SeedMasterMessageFormat(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasterMessageFormat>().HasData(
                    new MasterMessageFormat() { Id = 1, FormatType = "Registration", Remark = "Registration Type", IsActive = true },
                    new MasterMessageFormat() { Id = 2, FormatType = "OTP", Remark = "OTP Type", IsActive = true }
               );
        }
        public static void SeedWABAsDetails(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WABAsProvider>().HasData(
                   new WABAsProvider() { Id = 1, APIId = 1, APICode = "RNDPAY", ProviderName = "ROUNDPAY1", IsActive = true },
                   new WABAsProvider() { Id = 2, APIId = 2, APICode = "PAYTM", ProviderName = "PAYTM", IsActive = true }
              );
        }

        public static void SeedUserInfo(this ModelBuilder modelBuilder)
        {
            //PasswordHasher="Admin@123"
            modelBuilder.Entity<WhatsappUser>().HasData(
                    new WhatsappUser()
                    {
                        Id = 1,
                        UserName="admin@gmail.com",
                        NormalizedUserName="ADMIN@GMAIL.COM",
                        Email= "admin@gmail.com",
                        NormalizedEmail= "ADMIN@GMAIL.COM",
                        EmailConfirmed= true,
                        PasswordHash= "AQAAAAEAACcQAAAAEAyx8BWY/9CpOotUEyCWW7oPN/2HbxyjdkQSxPku80tUrHaumc7f45SExD+xgPy9MA==",
                        SecurityStamp= "DLE2VEKKAJXRDQLF3FHGWUKM6EO3JCZQ",
                        ConcurrencyStamp= "6879d998-a62e-4219-b24d-c47191f3c7a4",
                        PhoneNumber= "7777777777",
                        PhoneNumberConfirmed= true,
                        TwoFactorEnabled= false,
                        LockoutEnd= DateTimeOffset.Now, 
                        LockoutEnabled= false,  
                        AccessFailedCount= 0,
                        Name="Admin",
                        WID=0
                    }
               );
        }

        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                    new IdentityUserRole<int>()
                    {
                        UserId = 1,
                        RoleId = 1,
                    }
               );
        }
        public static void SeedMasterWebSite(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasterWebsite>().HasData(
                    new MasterWebsite()
                    {
                        Id = 1,
                        UserId = 1,
                        WebsiteName = "RoundpayWhatsapp.com",
                        Remark = "RoundpayWhatsapp.com",
                        IsActive = true,
                        EntryBy = 0,
                        ModifyBy = 0,
                        IsDeactivateByAdmin = false,
                        IsSSL = false
                    }
               );
        }
        public static void SeedCompanyProfile(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyProfile>().HasData(
                    new CompanyProfile()
                    {
                        Id = 1,
                        WID = 1,
                        Name = "Roundpay Techno Media Pvt Ltd",
                        MobileNo = "7777777777",
                        MobileNo2 = "",
                        Email = "Info@mobilet.com",
                        EmailSupport = "9807253370@gmail.com",
                        Facebook = "",
                        Twitter = "",
                        Instagram = "",
                        DeveloperKey = "9c34176d6b9710399c3cf3128acc6b24",
                        State = "NA",
                        CompanyAddress = "3/553 Station Road Near Aryan Restaurant Vivek Khand Gomtinagar Lucknow 226010",
                        PAN = "",
                        GSTIN = "",
                        HeaderTitle = "Mobile",
                        MobileSupport = "9807253370",
                        EmailTechnical = "support@roundpay.in"
                    }
               );
        }

    }
}
