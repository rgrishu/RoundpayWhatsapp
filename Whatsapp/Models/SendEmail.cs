using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class SendEmail
    {
        [Key]
        public int Int64 { get; set; }
        public string From { get; set; }   
        public string Recipients { get; set; }   
        public string Subject { get; set; }   
        public string Body { get; set; }   
        public bool IsSent { get; set; }   
        public int WID { get; set; }   
        public string EntryDate { get; set; }   
        public string ModifyDate { get; set; }   
       
    }
}
