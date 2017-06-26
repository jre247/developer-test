namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class RejectAppointmentCommand
    {
        public int PropertyId { get; set; }

        public int AppointmentId { get; set; }
    }
}