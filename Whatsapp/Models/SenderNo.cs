using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class SenderNo:BaseEntity
    {
        [Key]
        public int SenderNoID { get; set; }
        public int MobileNo { get; set; }
    }
}
