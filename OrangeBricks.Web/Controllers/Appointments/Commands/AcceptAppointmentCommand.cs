namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class AcceptAppointmentCommand
    {
        public int PropertyId { get; set; }

        public int AppointmentId { get; set; }
    }
}