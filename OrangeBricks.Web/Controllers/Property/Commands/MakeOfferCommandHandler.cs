using System;
using System.Collections.Generic;
using OrangeBricks.Web.Models;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Infrastructure;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeOfferCommandHandler
    {
        private readonly IOrangeBricksContext _context;
        private IdentityManagerBase _identityManager;

        public MakeOfferCommandHandler(IOrangeBricksContext context, IdentityManagerBase identityManager)
        {
            _context = context;
            _identityManager = identityManager;
        }

        public void Handle(MakeOfferCommand command)
        {
            var property = _context.Properties.Find(command.PropertyId);

            var offer = new Offer
            {
                Amount = command.Offer,
                Status = OfferStatus.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                BuyerUserId = _identityManager.GetLoggedInUserId()
            };

            if (property.Offers == null)
            {
                property.Offers = new List<Offer>();
            }
                
            property.Offers.Add(offer);
            
            _context.SaveChanges();
        }
    }
}