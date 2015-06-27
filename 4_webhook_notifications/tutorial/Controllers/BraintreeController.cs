using System.Linq;
using System.Web.Http;
using Braintree;

namespace tutorial.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class BraintreeController : ApiController {


        [Route("addons/{id:int}")]
        public IHttpActionResult GetAddons(int id)
        {
            return Ok(PaymentConstants.Gateway.AddOn.All().Where(i => i.Id.Contains(id.ToString())));
        }
        

        [Route("{id:int}")]
        [Route("GetCustomer/{id:int}")]
        public IHttpActionResult GetCustomer(int id) {
            var customer = PaymentConstants.Gateway.Customer.Find(id.ToString());
            return Ok(customer);
        }

        [Route("{id:int}/sub")]
        public IHttpActionResult GetCustomerSub(int id) {
            var customer = PaymentConstants.Gateway.Customer.Find(id.ToString()).CreditCards.First().Subscriptions;
            return Ok(customer);
        }


        [Route("{customerId:int}/addons")]
        public IHttpActionResult GetCustomerAddons(int customerId) {
            var customer = PaymentConstants.Gateway.Customer.Find(customerId.ToString());
            return Ok(customer);
        }

        public IHttpActionResult GetDiscounts() {
            return Ok(PaymentConstants.Gateway.Discount.All());
        }

    }
}