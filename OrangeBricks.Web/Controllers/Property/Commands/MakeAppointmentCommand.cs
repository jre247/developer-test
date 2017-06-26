using System;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeAppointmentCommand
    {
        public int PropertyId { get; set; }

        public DateTime AppointmentTime { get; set; }
    }
}