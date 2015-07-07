using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Braintree;

namespace tutorial.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(FormCollection collection)
        {
            var memberId = "123ASD";
            var request = new CustomerRequest
                          {
                              //Id = memberId, // Use member id
                              Email = collection["email"],
                              PaymentMethodNonce = collection["payment_method_nonce"], // Payment details
                              FirstName = collection["first_name"],
                              LastName = collection["last_name"],
                              CustomFields = new Dictionary<string, string>
                                             {
                                                 {"memberid", memberId}
                                             }
                          };

            var result = PaymentConstants.Gateway.Customer.Create(request);

            if(result.IsSuccess())
            {
                // Todo - Store info in DB

                var customer = result.Target;
                ViewData["CustomerName"] = customer.FirstName + " " + customer.LastName;
                ViewData["CustomerId"] = customer.Id;
                ViewData["PaymentMethodToken"] = customer.DefaultPaymentMethod.Token;

                var paymentMethodToken = customer.DefaultPaymentMethod.Token;

                var subResult = SubscriptionManager.SubResult(paymentMethodToken);

                ViewData["Message"] = String.Format("Subscription success: {0} <BR>Status: {1}", subResult.IsSuccess(),
                    subResult.Target.Status);
            } else
            {
                ViewData["Message"] = result.Message;
            }

            return View();
        }
    }
}