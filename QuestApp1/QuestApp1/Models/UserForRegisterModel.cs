using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace QuestApp1.Models
{
    public class UserForRegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, MaxLength(20), EmailAddress]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
