namespace Whatsapp.Models.ViewModel
{
    public class Email
    {
    }
    public class EmailSettingswithFormat
    {
        public int ID { get; set; }
        public int Statuscode { get; set; }
        public string Msg { get; set; }
        public int FormatID { get; set; }
        public string EmailTemplate { get; set; }
        public string Subject { get; set; }
        public bool IsEnableEmail { get; set; }
        public string FromEmail { get; set; }
        public string SaleEmail { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public bool IsSSL { get; set; }
        public string MailUserID { get; set; }
        public string Password { get; set; }
        public int WID { get; set; }
    }
}
