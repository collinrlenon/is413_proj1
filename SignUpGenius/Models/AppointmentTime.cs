using System;
using System.ComponentModel.DataAnnotations;

namespace SignUpGenius.Models
{
    //Sets up the appointment time model to handle the available times
    public class AppointmentTime
    {
        [Key]
        public int TimeID { get; set; }

        public string Day { get; set; }

        public string Time { get; set; }

        public bool Scheduled { get; set; }
    }
}