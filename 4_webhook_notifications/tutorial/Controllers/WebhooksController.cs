using System;
using System.Web.Mvc;

namespace tutorial.Controllers
{
    public class WebhooksController : Controller
    {
        public ActionResult Accept()
        {
            if(Request.HttpMethod != "POST")
            {
                //                var asd = new HttpResponseMessage() {
                //                    Content = new StringContent(
                //                        "<strong>test</strong>",
                //                        Encoding.UTF8,
                //                        "text/html"
                //                    )
                //                };
                //var content = Json(PaymentConstants.Gateway.WebhookNotification.Verify(Request.QueryString["bt_challenge"]), JsonRequestBehavior.AllowGet);
                var content = Content(PaymentConstants.Gateway.WebhookNotification.Verify(Request.QueryString["bt_challenge"]));
                return content;
            }

            var webhookNotification = PaymentConstants.Gateway.WebhookNotification.Parse(
                Request.Params["bt_signature"],
                Request.Params["bt_payload"]);

            if(webhookNotification.Timestamp == null)
            {
                return new HttpStatusCodeResult(200);
            }

            var message = String.Format("[Webhook Received {0}] | Kind: {1} | Subscription: {2}",
                webhookNotification.Timestamp.Value,
                webhookNotification.Kind,
                webhookNotification.Subscription.Id);

            Console.WriteLine(message);

            return new HttpStatusCodeResult(200);
        }
    }
}