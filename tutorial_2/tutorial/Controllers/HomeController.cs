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
            ViewData["TrData"] = Constants.Gateway.TrData(
                new CustomerRequest { },
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
            Result<Customer> result = Constants.Gateway.TransparentRedirect.ConfirmCustomer(Request.Url.Query);
            if (result.IsSuccess())
            {
                ViewData["Message"] = result.Target.Email;
            }
            else
            {
                ViewData["Message"] = string.Join(", ", result.Errors.DeepAll());
            }
            return View();
        }
    }

}
