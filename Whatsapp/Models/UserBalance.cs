namespace Whatsapp.Models
{
    public class UserBalance : BaseEntity
    {
        public int UserId { get; set; }
        public double Balance { get; set; }
        public string ModifyBy { get; set; }
    }
}
