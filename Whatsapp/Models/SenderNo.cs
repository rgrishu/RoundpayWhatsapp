using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class SenderNo:BaseEntity
    {
        public int MobileNo { get; set; }
        public int ApiID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsAutoReply { get; set; }
        public bool WID { get; set; }
    }
}
