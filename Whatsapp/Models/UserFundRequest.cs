using System.ComponentModel.DataAnnotations.Schema;
using Whatsapp.Models.Data;

namespace Whatsapp.Models
{
    public class UserFundRequest : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public WhatsappUser WhatsappUser { get; set; }
        public double RequestedAmount { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string EntryBy { get; set; }
        public string ModifyBy { get; set; }
        [NotMapped]
        public string LoggedInUserId { get; set; }

    }
}
