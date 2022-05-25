using System;

namespace Whatsapp.Models
{
    public class UserPackageDetail : BaseEntity
    {
        public int UserId { get; set; }
        public Int64 MasterPackageId { get; set; }
        public MasterPackage MasterPackage { get; set; }
        public string EntryBy { get; set; }
        public string ModifyBy { get; set; }
    }
}
