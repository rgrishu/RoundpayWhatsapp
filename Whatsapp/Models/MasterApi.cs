namespace Whatsapp.Models
{
    public class MasterApi
    {
        public int ApiID { get; set; } 
        public int ApiTypeID { get; set; } 
        public string ApiName { get; set; }
        public string ApiCode { get; set; }
        public string ApiBaseUrl { get; set; }
        public string ApiMethod { get; set; }
        public string IsActive { get; set; }
        public string IsDefault { get; set; }
        public string CreatedOn { get; set; }
        public string ModifyOn { get; set; }
    }
}
