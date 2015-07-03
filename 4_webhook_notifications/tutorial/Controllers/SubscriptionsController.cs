using System.Web.Mvc;
using Braintree;
using Braintree.Exceptions;

namespace tutorial.Controllers
{
    public class SubscriptionsController : Controller
    {
        public ActionResult Create(string id)
        {
            try
            {
                var customer = PaymentConstants.Gateway.Customer.Find(id);
                //var paymentMethodToken = customer.CreditCards[0].Token;
                var paymentMethodToken = customer.DefaultPaymentMethod.Token;


                var result = SubscriptionManager.SubResult(paymentMethodToken);

                return Content("Subscription Status " + result.Target.Status);
            } catch(NotFoundException e)
            {
                return Content("No customer found for id: " + id);
            }
        }
    }
}