using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsapp.Models
{
    public class MasterService
    {
        [Key]
        public Int64 ServiceID
        {
            get;
            set;
        }
        public string ServiceName { get; set; }
        public string SCode { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeature { get; set; }
        [NotMapped]
        public bool CheckIsFeature { get; set; }
    }
}
