using OrangeBricks.Web.Models;
using System;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class PropertyViewModel
    {
        public string StreetName { get; set; }
        public string Description { get; set; }
        public int NumberOfBedrooms { get; set; }
        public string PropertyType { get; set; }
        public int Id { get; set; }
        public bool IsListedForSale { get; set; }
        public PropertyOfferViewModel Offer { get; set; }
        public PropertyAppointmentViewModel Appointment { get; set; }

    }

    public class PropertyOfferViewModel
    {
        public bool IsAccepted { get; set; }
        public DateTime? AcceptDate { get; internal set; }
    }

    public class PropertyAppointmentViewModel
    {
        public DateTime? AppointmentTime { get; internal set; }
        public bool IsAccepted { get; internal set; }
    }
}