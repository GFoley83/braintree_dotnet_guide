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
            Environment = Braintree.Environment.SANDBOX,
            MerchantId = "your_merchant_id",
            PublicKey = "your_public_key",
            PrivateKey = "your_private_key"
        };
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTransaction(FormCollection collection)
        {
            TransactionRequest request = new TransactionRequest
            {
                Amount = 1000.0M,
                CreditCard = new TransactionCreditCardRequest
                {
                    Number = collection["number"],
                    CVV = collection["cvv"],
                    ExpirationMonth = collection["month"],
                    ExpirationYear = collection["year"]
                },
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = Constants.Gateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                Transaction transaction = result.Target;
                ViewData["TransactionId"] = transaction.Id;
            }
            else
            {
                ViewData["Message"] = result.Message;
            }

            return View();
        }
    }
}
