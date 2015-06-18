using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Braintree;

namespace braintree_tutorial.Controllers
{
    public class SubscriptionsController : Controller
    {
        public ActionResult Create(string id)
        {
            try
            {
                Customer customer = Constants.Gateway.Customer.Find(id);
                string paymentMethodToken = customer.CreditCards[0].Token;

                SubscriptionRequest request = new SubscriptionRequest
                {
                    PaymentMethodToken = paymentMethodToken,
                    PlanId = "15PlanMonthlyWithTrial",
                    AddOns = new AddOnsRequest()
                    {
                        Add = new AddAddOnRequest[] {
                          new AddAddOnRequest{
                            InheritedFromId = "15seat",
                            Quantity = 5,
                            //Amount = 15.00M // Override stored $ amount
                          }
                        }
                    }

                };

                Result<Subscription> result = Constants.Gateway.Subscription.Create(request);

                return Content("Subscription Status " + result.Target.Status);
            }
            catch (Braintree.Exceptions.NotFoundException e)
            {
                return Content("No customer found for id: " + id);
            }

        }
    }
}
