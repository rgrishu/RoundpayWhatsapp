﻿namespace Whatsapp.Models.ViewModel
{
    public class Users:BaseEntity
    {
        public int UserID { get; set; }
        public string Role { get; set; }
        public string Name { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Email { get; set; } 
       
    }
}
