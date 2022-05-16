namespace Whatsapp.Models
{
    public class MessageTemplate:BaseEntity
    {
        public int FormatID { get; set; }
        public bool IsEnableSMS { get; set; }
        public string TemplateID { get; set; }
        public string SMSTemplate { get; set; }
        public bool IsEnableEmail { get; set; }
        public string Subject { get; set; }
        public string EmailTemplate { get; set; }
        public bool IsEnableWhatsapp { get; set; }
        public string WhatsappTemplate { get; set; }
        public int WID { get; set; }
        public int EntryBy { get; set; }
        public int ModifyBy { get; set; }
    }
}
