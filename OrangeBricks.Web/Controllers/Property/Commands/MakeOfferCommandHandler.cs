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
        private IdentityManager _identityManager;

        public MakeOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
            _identityManager = new IdentityManager();
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
                UserId = _identityManager.GetLoggedInUserId()
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