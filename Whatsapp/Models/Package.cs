using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsapp.Models
{
    public class Package
    {
        [Key]
        public Int64 PackageID
        {
            get;
            set;
        }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public Int64 MasterPackageID { get; set; }
        [ForeignKey("MasterPackageID")]
        public MasterService MasterService { get; set; }
        public Int64 ServiceID { get; set; }
        [ForeignKey("ServiceID")]
        public MasterPackage MasterPackage { get; set; }
    }
}
