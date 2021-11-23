using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsapp.Models
{
   
    public class State:BaseEntity
    {
        public string StateName { get; set; }
    }
 
   
}
