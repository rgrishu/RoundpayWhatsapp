using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Whatsapp.Models
{
    public class MasterApi:BaseEntity
    {
        public int ApiTypeID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string BaseUrl { get; set; }
        public string Method { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
