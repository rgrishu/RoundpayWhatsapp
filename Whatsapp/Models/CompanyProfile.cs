namespace Whatsapp.Models
{
    public class CompanyProfile : BaseEntity
    {
        public int WID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string MobileNo2 { get; set; }
        public string Email { get; set; }
        public string EmailSupport { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string DeveloperKey{ get; set; }
        public string State { get; set; }
        public string CompanyAddress { get; set; }
        public string PAN { get; set; }
        public string GSTIN { get; set; }
        public string HeaderTitle { get; set; }
        public string MobileSupport { get; set; }
        public string EmailTechnical { get; set; }
    }
}
