using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class EmailSetting:BaseEntity
    {
        public string FromEmail { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public int WID { get; set; }
        public string EntryBy { get; set; }
        public string ModifyBy { get; set; }
        public bool IsActive { get; set; }
        public string IsEmailVerified { get; set; }
        public bool IsSSL { get; set; }
        public bool IsDefault { get; set; }
       
    }
}
