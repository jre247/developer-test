using System;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class AcceptAppointmentCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public AcceptAppointmentCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(AcceptAppointmentCommand command)
        {
            var offer = _context.Appointments.Find(command.AppointmentId);

            offer.UpdatedAt = DateTime.Now;
            offer.Status = AppointmentStatus.Accepted;

            _context.SaveChanges();
        }
    }
}