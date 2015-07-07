using Braintree;

namespace tutorial.Controllers
{
    public class PaymentConstants
    {
        public static BraintreeGateway Gateway = new BraintreeGateway
        {
            Environment = Environment.SANDBOX,
            MerchantId = "mhg3f3x2ppkfqckj",
            PublicKey = "5kvnpr5fnn59w7fh",
            PrivateKey = "8c1f5f61380e5cc49ba7d197cf7a5108"
        };
    }
}