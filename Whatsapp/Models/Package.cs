using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsapp.Models
{
    public class Package : BaseEntity
    {
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeature { get; set; }
        public bool IsChargeAfterHitExceed { get; set; }
        public bool IsDirectChargeable { get; set; }
        public string Charge { get; set; }
        public Int64 MasterPackageID { get; set; }
        public MasterPackage MasterPackage { get; set; }
        public Int64? ServiceID { get; set; }
        [ForeignKey("ServiceID")]
        public MasterService MasterService { get; set; }
        public Int64? FeatureID { get; set; }
        [ForeignKey("FeatureID")]
        public MasterServiceFeatures MasterServiceFeatures { get; set; }

    }
    public class PackageView : Package
    {
        public List<MasterService> MasterServices { get; set; }
        public List<MasterPackage> MasterPackages { get; set; }
        public List<Int64> UserPuchasedPackageIds { get; set; }
        public List<Package> Packages { get; set; }
    }
}
