using System;
using System.Collections.Generic;
using OrangeBricks.Web.Models;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Infrastructure;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeAppointmentCommandHandler
    {
        private readonly IOrangeBricksContext _context;
        private IdentityManagerBase _identityManager;

        public MakeAppointmentCommandHandler(IOrangeBricksContext context, IdentityManagerBase identityManager)
        {
            _context = context;
            _identityManager = identityManager;
        }

        public void Handle(MakeAppointmentCommand command)
        {
            var property = _context.Properties.Find(command.PropertyId);

            var appointment = new Appointment
            {
                AppointmentTime = command.AppointmentTime,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                BuyerUserId = _identityManager.GetLoggedInUserId()
            };

            if (property.Appointments == null)
            {
                property.Appointments = new List<Appointment>();
            }
                
            property.Appointments.Add(appointment);
            
            _context.SaveChanges();
        }
    }
}