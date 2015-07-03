using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace tutorial.Controllers {
    public class WebhookController : ApiController {

        [HttpGet]
        [HttpPost]
        public IHttpActionResult Accept() {

            if(HttpContext.Current.Request.HttpMethod != "POST") {
                var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
                var bt = allUrlKeyValues.SingleOrDefault(x => x.Key == "bt_challenge").Value;
                var verify = PaymentConstants.Gateway.WebhookNotification.Verify(bt);

                var responseMsg = new HttpResponseMessage(HttpStatusCode.OK) {
                    Content = new StringContent(verify, Encoding.UTF8, "text/plain")
                };

                return ResponseMessage(responseMsg);
            }

            return BadRequest("nope");
        }
    }
}