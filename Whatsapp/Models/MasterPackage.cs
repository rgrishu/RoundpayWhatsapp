using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsapp.Models
{
    public class MasterPackage:BaseEntity
    {
        public string PackageName { get; set; }
        public double Cost { get; set; }
        public int ValidityInDays { get; set; }
        public string Status { get; set; }
        public Int32 HitCount { get; set; }
    }
}
