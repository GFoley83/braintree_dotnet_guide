using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Braintree;
using System.Diagnostics;

namespace braintree_tutorial.Controllers
{
    public class WebhooksController : Controller
    {
        public ActionResult Accept()
        {
            if (Request.HttpMethod == "POST")
            {
                WebhookNotification webhookNotification = Constants.Gateway.WebhookNotification.Parse(
                    Request.Params["bt_signature"],
                    Request.Params["bt_payload"]
                    );

                string message = string.Format(
                   "[Webhook Received {0}] | Kind: {1} | Subscription: {2}",
                    webhookNotification.Timestamp.Value,
                    webhookNotification.Kind,
                    webhookNotification.Subscription.Id
                    );

                System.Console.WriteLine(message);
                return new HttpStatusCodeResult(200);
            }
            else
            {
                return Content(Constants.Gateway.WebhookNotification.Verify(Request.QueryString["bt_challenge"]));
            }
            
        }
    }
}
