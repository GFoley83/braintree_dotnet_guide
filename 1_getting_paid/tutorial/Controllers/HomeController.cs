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
            ViewData["TrData"] = Constants.Gateway.Transaction.SaleTrData(
                new TransactionRequest
                {
                    Amount = 1000.00M,
                    Options = new TransactionOptionsRequest
                    {
                        SubmitForSettlement = true
                    }

                },
                "http://localhost:49283/result"
            );
            ViewData["TransparentRedirectURL"] = Constants.Gateway.TransparentRedirect.Url;
            return View();
        }
    }
    public class ResultController : Controller
    {
        public ActionResult Index()
        {
            Result<Transaction> result = Constants.Gateway.TransparentRedirect.ConfirmTransaction(Request.Url.Query);
            if (result.IsSuccess())
            {
                Transaction transaction = result.Target;
                ViewData["Message"] = transaction.Status;
            }
            else
            {
                ViewData["Message"] = string.Join(", ", result.Errors.DeepAll());
            }
            return View();
        }
    }

}
