using System.Web.Mvc;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Offers.Builders;
using OrangeBricks.Web.Controllers.Offers.Commands;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Infrastructure;

namespace OrangeBricks.Web.Controllers.Offers
{
    [OrangeBricksAuthorize(Roles = "Seller")]
    public class AppointmentsController : Controller
    {
        private readonly IOrangeBricksContext _context;

        public AppointmentsController(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ActionResult OnProperty(int id)
        {
            var builder = new AppointmentsOnPropertyViewModelBuilder(_context, new IdentityManager());
            var viewModel = builder.Build(id);

            return View(viewModel);
        }

        [HttpPost]        
        public ActionResult Accept(AcceptAppointmentCommand command)
        {
            var handler = new AcceptAppointmentCommandHandler(_context);

            handler.Handle(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }

        [HttpPost]
        public ActionResult Reject(RejectAppointmentCommand command)
        {
            var handler = new RejectAppointmentCommandHandler(_context);

            handler.Handle(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }
    }
}