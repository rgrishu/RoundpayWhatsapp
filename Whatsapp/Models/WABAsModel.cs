using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class WABAsModel
    {
        [Key]
        public int Id { get; set; }
        public int APIId { get; set; }
        public string APICode { get; set; }
        public string ProviderName { get; set; }
        public bool IsActive { get; set; }
    }
}
