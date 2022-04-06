using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BartenderBuddy.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
    }
}