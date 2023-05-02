using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Samples;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;

namespace Samples.AuthorizeIntentExamples
{
    public class AuthorizeOrderSample
    {

        //This function can be used to perform authorization on the approved order.
        public async static Task<HttpResponse> AuthorizeOrder(string OrderId, bool debug = false)
        {
            var request = new OrdersAuthorizeRequest(OrderId);
            request.Prefer("return=representation");
            request.RequestBody(new AuthorizeRequest());
            var response = await PayPalClient.client().Execute(request);


            return response;
        }

        //static void Main(string[] args)
        //{
        //    string OrderId = "<<REPLACE-WITH-APPROVED-ORDER-ID>>";
        //    AuthorizeOrder(OrderId, true).Wait();
        //}
    }
}
