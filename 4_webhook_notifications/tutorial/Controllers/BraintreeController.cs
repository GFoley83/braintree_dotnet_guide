using System;
using System.Linq;
using System.Web.Http;

namespace tutorial.Controllers
{
    [RoutePrefix("api/Braintree")]
    public class BraintreeController : ApiController
    {
        [Route("token/{id:int?}")]
        public IHttpActionResult GetNonce(int id = 0)
        {
            try
            {
                return Ok(PaymentConstants.Gateway.ClientToken.generate());
            } catch(Exception)
            {
                return BadRequest("Could not contact payment gateway for token.");
            }
        }

        [Route("addons/{id:int}")]
        public IHttpActionResult GetAddons(int id)
        {
            return Ok(PaymentConstants.Gateway.AddOn.All().Where(i => i.Id.Contains(id.ToString())));
        }

        [Route("{id:int}")]
        [Route("GetCustomer/{id:int}")]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = PaymentConstants.Gateway.Customer.Find(id.ToString());
            return Ok(customer);
        }

        [Route("{id:int}/sub")]
        public IHttpActionResult GetCustomerSub(int id)
        {
            var customer = PaymentConstants.Gateway.Customer.Find(id.ToString()).CreditCards.First().Subscriptions;
            return Ok(customer);
        }

        [Route("{customerId:int}/addons")]
        public IHttpActionResult GetCustomerAddons(int customerId)
        {
            var customer = PaymentConstants.Gateway.Customer.Find(customerId.ToString());
            return Ok(customer);
        }

        public IHttpActionResult GetDiscounts()
        {
            return Ok(PaymentConstants.Gateway.Discount.All());
        }
    }
}