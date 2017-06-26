using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class MakeAppointmentViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MakeAppointmentViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public MakeAppointmentViewModel Build(int id)
        {
            var property = _context.Properties.Find(id);

            return new MakeAppointmentViewModel
            {
                PropertyId = property.Id,
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                AppointmentTime = DateTime.Now
            };
        }
    }
}