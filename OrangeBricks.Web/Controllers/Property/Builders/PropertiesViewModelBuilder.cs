using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Infrastructure;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class PropertiesViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;
        private readonly IdentityManagerBase _identityManager;

        public PropertiesViewModelBuilder(IOrangeBricksContext context, IdentityManagerBase identityManager)
        {
            _context = context;
            _identityManager = identityManager;
        }

        public PropertiesViewModel Build(PropertiesQuery query)
        {
            var properties = _context.Properties
                .Where(p => p.IsListedForSale);

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                properties = properties.Where(x => x.StreetName.Contains(query.Search) 
                    || x.Description.Contains(query.Search));
            }

            return new PropertiesViewModel
            {
                Properties = properties
                    .ToList()
                    .Select(p => MapViewModel(p))
                    .ToList(),
                Search = query.Search
            };
        }

        private PropertyViewModel MapViewModel(Models.Property property)
        {
            return new PropertyViewModel
            {
                Id = property.Id,
                StreetName = property.StreetName,
                Description = property.Description,
                NumberOfBedrooms = property.NumberOfBedrooms,
                PropertyType = property.PropertyType,
                Offer = GetPropertyOffer(property),
                Appointment = GetPropertyAppointment(property)
            };
        }

        private PropertyOfferViewModel GetPropertyOffer(Models.Property property)
        {
            var userId = _identityManager.GetLoggedInUserId();
            var offerViewModel = new PropertyOfferViewModel();

            if (property.Offers == null)
                return offerViewModel;

            var offer = property.Offers.FirstOrDefault(o => o.UserId == userId);
            if (offer == null)
                return offerViewModel;

            offerViewModel.IsAccepted = offer.Status == OfferStatus.Accepted;
            offerViewModel.AcceptDate = offer.UpdatedAt;
            return offerViewModel;
        }

        private PropertyAppointmentViewModel GetPropertyAppointment(Models.Property property)
        {
            var userId = _identityManager.GetLoggedInUserId();
            var appointmentViewModel = new PropertyAppointmentViewModel();

            if (property.Appointments == null)
                return appointmentViewModel;

            var appointment = property.Appointments.FirstOrDefault(o => o.BuyerUserId == userId);
            if (appointment == null)
                return appointmentViewModel;

            appointmentViewModel.IsAccepted = appointment.Status == AppointmentStatus.Accepted;
            appointmentViewModel.AppointmentTime = appointment.UpdatedAt;
            return appointmentViewModel;
        }
    }
}