using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class ApiRequestResponseLog
    {
        [Key]
        public int RequestID { get; set; }
        public string RequestName { get; set; }
        public int UserID { get; set; }
        public string RequestUrl { get; set; }
        public string RequestData { get; set; }
        public string Response { get; set; }
        public string Remark { get; set; }
        public string CreatedOn { get; set; }
    }
}
