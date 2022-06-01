using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Whatsapp.Models
{
    public class Ledger : BaseEntity
    {
        public int UserId { get; set; }
        public Int64? MasterPackageId { get; set; }
        [ForeignKey("MasterPackageId")]
        public MasterPackage MasterPackage { get; set; }
        public Int64? ServiceId { get; set; }
        [NotMapped]
        public MasterService MasterService { get; set; }
        public Int64? FeatureId { get; set; }
        [NotMapped]
        public MasterServiceFeatures MasterServiceFeatures { get; set; }
        public double OpeningBalance { get; set; }
        public double Amount { get; set; }
        public double ClosingBalance { get; set; }
        public string TransactionType { get; set; }
        public string EntryBy { get; set; }
        public string ModifyBy { get; set; }

    }
}
