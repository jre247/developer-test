using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Infrastructure;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    public class AppointmentsOnPropertyViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;
        private IdentityManagerBase _identityManager;

        public AppointmentsOnPropertyViewModelBuilder(IOrangeBricksContext context, IdentityManagerBase identityManager)
        {
            _context = context;
            _identityManager = identityManager;
        }

        public AppointmentsOnPropertyViewModel Build(int id)
        {
            var property = _context.Properties
                .Where(p => p.Id == id)
                .Include(x => x.Offers)
                .SingleOrDefault();

            var appointments = property.Appointments ?? new List<Appointment>();

            return new AppointmentsOnPropertyViewModel
            {
                HasAppointments = appointments.Any(),
                Appointments = appointments.Select(x => new AppointmentViewModel
                {
                    Id = x.Id,
                    AppointmentTime = x.AppointmentTime,
                    CreatedAt = x.CreatedAt,
                    IsPending = x.Status == AppointmentStatus.Pending,
                    Status = x.Status.ToString()
                }),
                PropertyId = property.Id, 
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                NumberOfBedrooms = property.NumberOfBedrooms
            };
        }
    }
}