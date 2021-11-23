using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsapp.Models.Data
{
    public class City:BaseEntity
    {   
        public int StateID { get; set; }
        public string CityName { get; set; }
    }
}
