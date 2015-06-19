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
                var customer = Constants.Gateway.Customer.Find(id);
                var paymentMethodToken = customer.CreditCards[0].Token;

                var request = new SubscriptionRequest
                              {
                                  PaymentMethodToken = paymentMethodToken,
                                  PlanId = "15PlanMonthlyWithTrial",
                                  AddOns = new AddOnsRequest()
                                           {
                                               Add = new[]
                                                     {
                                                         new AddAddOnRequest
                                                         {
                                                             InheritedFromId = "15seat",
                                                             Quantity = 5,
                                                             //Amount = 15.00M // Override stored $ amount
                                                         }
                                                     }
                                           }
                              };

                var result = Constants.Gateway.Subscription.Create(request);

                return Content("Subscription Status " + result.Target.Status);
            } catch(NotFoundException e)
            {
                return Content("No customer found for id: " + id);
            }
        }
    }
}