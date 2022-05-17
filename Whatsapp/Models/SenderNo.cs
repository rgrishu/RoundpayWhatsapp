using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Whatsapp.Models
{
    public class SenderNo:BaseEntity
    {
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsAutoReply { get; set; }
        public int WID { get; set; }
        public Int64 ApiID { get; set; }
        [ForeignKey("ApiID")]
        public MasterApi MasterApi { get; set; }
    }
}
