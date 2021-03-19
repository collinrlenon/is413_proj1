using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignUpGenius.Models
{
    //Set up of the form model -- basically just the appointment model
    public class FormModel
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 50)]
        public int Size { get; set; }
        [Required]
        //[RegularExpression(@"\b[A - Z0 - 9._ % +-] +@[A-Z0-9.-]+\.[A-Z]{2,}\b", ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        [RegularExpression(@"\d{3}-\d{3}-\d{4}", ErrorMessage = "Please enter a valid phone number.")]
        public string? Phone { get; set; }
    }
}
