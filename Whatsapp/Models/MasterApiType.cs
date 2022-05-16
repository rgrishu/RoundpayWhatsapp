using System.ComponentModel.DataAnnotations;

namespace Whatsapp.Models
{
    public class MasterApiType
    {
        [Key]
        public int ApiTypeID { get; set; } 
        public string ApiTypeName { get; set; } 
    }
}
