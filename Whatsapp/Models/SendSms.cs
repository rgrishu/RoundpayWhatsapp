using System;
using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class SendSms
    {
        [Key]
         public Int64 ID { get; set; }
        public string APIID { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public string TransactionID { get; set; }
        public string ResponseID { get; set; }
        public string Response { get; set; }
        public string EntryDate { get; set; }
        public string IsRead { get; set; }
        public int WID { get; set; }
        public string ModifyDate { get; set; }
    }
}
