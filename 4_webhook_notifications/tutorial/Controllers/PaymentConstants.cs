using Braintree;

namespace tutorial.Controllers
{
    public class PaymentConstants
    {
        public static BraintreeGateway Gateway = new BraintreeGateway
                                                 {
                                                     Environment = Environment.SANDBOX,
                                                     MerchantId = "4d2y6xzyymry4y6r",
                                                     PublicKey = "brsn9qmdh37vk3vn",
                                                     PrivateKey = "ffd8a3c3c642dd017940d52a8cb2af93"
                                                 };
    }
}