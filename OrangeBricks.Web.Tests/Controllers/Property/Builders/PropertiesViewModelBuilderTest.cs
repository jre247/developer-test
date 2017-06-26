using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Models;
using System;
using OrangeBricks.Web.Infrastructure;
using Moq;

namespace OrangeBricks.Web.Tests.Controllers.Property.Builders
{
    public static class ExtentionMethods
    {
        public static IDbSet<T> Initialize<T>(this IDbSet<T> dbSet, IQueryable<T> data) where T : class
        {
            dbSet.Provider.Returns(data.Provider);
            dbSet.Expression.Returns(data.Expression);
            dbSet.ElementType.Returns(data.ElementType);
            dbSet.GetEnumerator().Returns(data.GetEnumerator());
            return dbSet;
        }
    }

    [TestFixture]
    public class PropertiesViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        private PropertiesViewModelBuilder GetPropertiesViewModelBuilder(string userId)
        {
            var mock = new Mock<IdentityManagerBase>();
            mock.Setup(m => m.GetLoggedInUserId()).Returns(userId);
            return new PropertiesViewModelBuilder(_context, mock.Object);
        }

        [Test]
        public void BuildShouldReturnPropertiesWithMatchingStreetNamesWhenSearchTermIsProvided()
        {
            // Arrange
            var builder = GetPropertiesViewModelBuilder("user-id-1");

            var properties = new List<Models.Property>{
                new Models.Property{ StreetName = "Smith Street", Description = "", IsListedForSale = true },
                new Models.Property{ StreetName = "Jones Street", Description = "", IsListedForSale = true}
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var query = new PropertiesQuery
            {
                Search = "Smith Street"
            };

            // Act
            var viewModel = builder.Build(query);

            // Assert
            Assert.That(viewModel.Properties.Count, Is.EqualTo(1));
        }

        [Test]
        public void BuildShouldReturnPropertiesWithMatchingDescriptionsWhenSearchTermIsProvided()
        {
            // Arrange
            var builder = GetPropertiesViewModelBuilder("user-id-1");

            var properties = new List<Models.Property>{
                new Models.Property{ StreetName = "", Description = "Great location", IsListedForSale = true },
                new Models.Property{ StreetName = "", Description = "Town house", IsListedForSale = true }
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var query = new PropertiesQuery
            {
                Search = "Town"
            };

            // Act
            var viewModel = builder.Build(query);

            // Assert
            Assert.That(viewModel.Properties.Count, Is.EqualTo(1));
            Assert.That(viewModel.Properties.All(p => p.Description.Contains("Town")));
        }

        [Test]
        public void BuildShouldShowAcceptOfferMessageWhenOfferIsAccepted()
        {
            // Arrange
            var userId = "user-id-1";
            var builder = GetPropertiesViewModelBuilder(userId);
            var now = DateTime.Now;
            var properties = new List<Models.Property>{
                new Models.Property{
                    StreetName = "",
                    Description = "Great location",
                    IsListedForSale = true,
                    Id = 123,
                    Offers = new List<Models.Offer>
                    {
                        new Models.Offer
                        {
                            Status = OfferStatus.Accepted,
                            UpdatedAt = now,
                            Amount = 100,
                            UserId = userId,
                            Id = 1
                        }
                    }
                },
                new Models.Property{ StreetName = "", Description = "Town house", IsListedForSale = true }
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var query = new PropertiesQuery();

            // Act
            var viewModel = builder.Build(query);

            // Assert
            var propertyToCompare = viewModel.Properties.ElementAt(0);
            Assert.That(propertyToCompare.Offer.IsAccepted, Is.EqualTo(true));
            Assert.That(propertyToCompare.Offer.AcceptDate, Is.EqualTo(now));
        }
    }
}
