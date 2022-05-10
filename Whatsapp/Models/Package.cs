using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Int64 MasterPackageID
        {
            get;
            set;
        }
        public Int64 ServiceID
        {
            get;
            set;
        }
        public string Status { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
