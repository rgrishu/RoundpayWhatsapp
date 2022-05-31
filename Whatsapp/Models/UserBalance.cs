using System.ComponentModel.DataAnnotations.Schema;

namespace Whatsapp.Models
{
    public class UserBalance : BaseEntity
    {
        public int UserId { get; set; }
        public double Balance { get; set; }
        public string ModifyBy { get; set; }
        [NotMapped]
        public double PreviousBalance { get; set; }
    }
}
