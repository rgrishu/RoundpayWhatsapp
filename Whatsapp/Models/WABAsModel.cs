using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Whatsapp.Models
{
    public class WABAsProvider
    {
        [Key]
        public int Id { get; set; }
        public int APIId { get; set; }
        public string APICode { get; set; }
        public string ProviderName { get; set; }
        public bool IsActive { get; set; }
    }
    public class WABAsNumber : BaseEntity
    {
        public int WABAsId { get; set; }
        [ForeignKey("WABAsId")]
        public WABAsProvider WABAsProvider { get; set; }
        public string Mobile { get; set; }
        public string Protocol { get; set; }
        public string CallbackURL { get; set; }
        public DateTime ScannedOn { get; set; }
    }
}
