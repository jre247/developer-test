using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public DateTime AppointmentTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string BuyerUserId { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}
