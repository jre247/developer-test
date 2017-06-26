using System;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class RejectAppointmentCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public RejectAppointmentCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(RejectAppointmentCommand command)
        {
            var offer = _context.Appointments.Find(command.AppointmentId);

            offer.UpdatedAt = DateTime.Now;
            offer.Status = AppointmentStatus.Rejected;

            _context.SaveChanges();
        }
    }
}