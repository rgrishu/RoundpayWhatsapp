using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class EmailSetting
    {
        [Key]
        public int ID { get; set; }
        public string FromEmail { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public string Port { get; set; }
        public int WID { get; set; }
        public string EntryByLT { get; set; }
        public string EntryBy { get; set; }
        public string EntryDate { get; set; }
        public string ModifyByLT { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public bool IsActive { get; set; }
        public string IsEmailVerified { get; set; }
        public bool IsSSL { get; set; }
        public string MailUserID { get; set; }
        public string IsDefault { get; set; }
       
    }
}
