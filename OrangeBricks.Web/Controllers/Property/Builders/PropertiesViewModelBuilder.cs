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
                Offer = GetPropertyOffer(property)
            };
        }

        private PropertyOfferViewModel GetPropertyOffer(Models.Property property)
        {
            var userId = _identityManager.GetLoggedInUserId();
            var propertyOffer = new PropertyOfferViewModel();

            if (property.Offers == null)
                return propertyOffer;

            var offer = property.Offers.FirstOrDefault(o => o.UserId == userId);
            if (offer == null)
                return propertyOffer;

            propertyOffer.IsAccepted = offer.Status == OfferStatus.Accepted;
            propertyOffer.AcceptDate = offer.UpdatedAt;
            return propertyOffer;
        }
    }
}