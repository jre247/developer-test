using System;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class MakeAppointmentViewModel
    {
        public string PropertyType { get; set; }
        public string StreetName { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int PropertyId { get; set; }
    }
}