using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class Contacts
    {
        [Key]
        public int ContactID { get; set; }
        public string MobileNo { get; set; }
    }
}
