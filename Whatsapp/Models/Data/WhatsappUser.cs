using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Whatsapp.Models.Data
{
    public class WhatsappUser : IdentityUser
    {
        public string Name { get; set; }
    }
 
   
}
