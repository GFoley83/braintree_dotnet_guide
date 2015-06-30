using Braintree;
using tutorial.Controllers;

namespace tutorial
{
    public class SubscriptionManager {
        public static Result<Subscription> SubResult(string paymentMethodToken)
        {
            // All trial flags below are needed to override the default trial settings
            var request = new SubscriptionRequest
                          {
                              PaymentMethodToken = paymentMethodToken,
                              //HasTrialPeriod = true,
                              //TrialDuration = 6,
                              //TrialDurationUnit = SubscriptionDurationUnit.MONTH,
                              PlanId = "RegularSeat",
                              AddOns = new AddOnsRequest()
                                       {
                                           Add = new[]
                                                 {
                                                     new AddAddOnRequest
                                                     {
                                                         InheritedFromId = "ExtraRegularSeat",
                                                         Quantity = 5,
                                                         //Amount = 15.00M // Override stored $ amount
                                                     }
                                                 }
                                       }
                          };

            var result = PaymentConstants.Gateway.Subscription.Create(request);
            return result;
        }
    }
}