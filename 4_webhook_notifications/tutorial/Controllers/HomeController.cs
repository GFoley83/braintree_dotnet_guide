using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Braintree;

namespace braintree_tutorial.Controllers
{
    public class Constants
    {
        public static BraintreeGateway Gateway = new BraintreeGateway
        {
            Environment = Braintree.Environment.DEVELOPMENT,
            MerchantId = "integration_merchant_id",
            PublicKey = "integration_public_key",
            PrivateKey = "integration_private_key"
        };
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(FormCollection collection)
        {
            CustomerRequest request = new CustomerRequest
            {
                FirstName = collection["first_name"],
                LastName = collection["last_name"],
                CreditCard = new CreditCardRequest
                {
                    BillingAddress = new CreditCardAddressRequest
                    {
                        PostalCode = collection["postal_code"]
                    },
                    Number = collection["number"],
                    ExpirationMonth = collection["month"],
                    ExpirationYear = collection["year"],
                    CVV = collection["cvv"]
                }
            };

            Result<Customer> result = Constants.Gateway.Customer.Create(request);
            if (result.IsSuccess())
            {
                Customer customer = result.Target;
                ViewData["CustomerName"] = customer.FirstName + " " + customer.LastName;
                ViewData["CustomerId"] = customer.Id;
            }
            else
            {
                ViewData["Message"] = result.Message;
            }

            return View();
        }
    }
}
