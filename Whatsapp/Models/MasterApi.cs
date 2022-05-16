using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class MasterApi:BaseEntity
    {
        public int ApiTypeID { get; set; } 
        public string Name { get; set; }
        public string Code { get; set; }
        public string BaseUrl { get; set; }
        public string Method { get; set; }
        public string IsActive { get; set; }
        public string IsDefault { get; set; }
        public string CreatedOn { get; set; }
        public string ModifyOn { get; set; }
    }
}
