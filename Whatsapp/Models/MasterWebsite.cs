namespace Whatsapp.Models
{
    public class MasterWebsite:BaseEntity
    {
        public int UserId { get; set; }
        public string WebsiteName { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public int EntryBy { get; set; }
        public int ModifyBy { get; set; }
        public bool IsDeactivateByAdmin { get; set; }
        public bool IsSSL { get; set; }
    }
}
