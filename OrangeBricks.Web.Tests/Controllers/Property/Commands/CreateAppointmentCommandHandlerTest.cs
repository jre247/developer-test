using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Infrastructure;
using Moq;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class CreateAppointmentCommandHandlerTest
    {
        private MakeAppointmentCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Property> _properties;
        private string _userId;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _properties = Substitute.For<IDbSet<Models.Property>>();
            _context.Properties.Returns(_properties);
            _userId = "123";
            var mock = new Mock<IdentityManagerBase>();
            mock.Setup(m => m.GetLoggedInUserId()).Returns(_userId);
            _handler = new MakeAppointmentCommandHandler(_context, mock.Object);
        }

        [Test]
        public void HandleShouldPropertyHaveAppointment()
        {
            // Arrange
            var command = new MakeAppointmentCommand
            {
                PropertyId = 1
            };

            var property = new Models.Property
            {
                Description = "Test Property",
                IsListedForSale = true
            };

            _properties.Find(1).Returns(property);

            // Act
            _handler.Handle(command);

            // Assert
            _context.Properties.Received(1).Find(1);
            _context.Received(1).SaveChanges();
            Assert.That(_properties.Find(1).Appointments.Count, Is.EqualTo(1));
        }
    }
}
