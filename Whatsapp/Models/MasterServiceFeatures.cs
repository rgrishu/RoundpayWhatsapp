using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsapp.Models
{
    public class MasterServiceFeatures
    {

        [Key]
        public Int64 FeatureID
        {
            get;
            set;
        }
        public string FeatureName { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public Int64 ServiceID { get; set; }

        [ForeignKey("ServiceID")]
        public MasterService MasterService { get; set; }

    }
}
