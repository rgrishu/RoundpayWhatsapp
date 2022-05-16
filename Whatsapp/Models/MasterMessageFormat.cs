
namespace Whatsapp.Models
{
    public class MasterMessageFormat:BaseEntity
    {
        public string FormatType { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
