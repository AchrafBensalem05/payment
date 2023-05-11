using PayoutsSdk.Payouts;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using System.Net;

namespace Stayin.Payment;

internal class PaymentHandler : IPaymentHandler
{
    public async Task<OrderCaptureResult> CapturePaymentOrder(string orderId)
    {
        var request = new OrdersCaptureRequest(orderId);
        request.Prefer("return=representation");
        request.RequestBody(new OrderActionRequest());

        try
        {
            var response = await PayPalClient.client().Execute(request);

            if(response.StatusCode != HttpStatusCode.OK)
                return new OrderCaptureResult() { Successful = false };

            return new OrderCaptureResult() { Successful = true };
        }
        catch(Exception ex)
        {
            return new OrderCaptureResult() { Successful = false };
        }

    }

    public async Task<OrderCreationResult> CreatePaymentOrder(GetPaidInfo paymentInfo)
    {
        OrderRequest orderRequest = new OrderRequest()
        {
            CheckoutPaymentIntent = "CAPTURE",
            ApplicationContext = new ApplicationContext
            {
                CancelUrl = paymentInfo.CancelUrl,
                ReturnUrl = paymentInfo.ReturnUrl,
            },
            PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest{
                        AmountWithBreakdown = new AmountWithBreakdown
                        {
                            CurrencyCode = paymentInfo.CurrencyCode,
                            Value = paymentInfo.Amount.ToString()
                        }

                    }
                }
        };

        var request = new OrdersCreateRequest();
        request.Headers.Add("prefer", "return=representation");
        request.RequestBody(orderRequest);

        try
        {
            var result = await PayPalClient.client().Execute(request);

            if(result.StatusCode != HttpStatusCode.OK)
                return new OrderCreationResult() { Successful = false };

            // Ask user to approve through approve link
            var order = result.Result<Order>();

            var approveLink = order.Links.Where(x => x.Rel == "approve").First().Href;

            return new OrderCreationResult() { ApproveLink = approveLink, OrderId = order.Id, Successful = true };
        }
        catch(HttpException ex)
        {
            return new OrderCreationResult() { Successful = false };
        }
    }

    public async Task<PaymentResult> Pay(UserPaymentInfo receiverInfo, PayUserInfo paymentInfo)
    {
        var body = new CreatePayoutRequest()
        {
            SenderBatchHeader = new SenderBatchHeader()
            {
                SenderBatchId = "Payouts_" + DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                EmailMessage = "Congrats on recieving" + paymentInfo.Amount + " " + paymentInfo.CurrencyCode,
                EmailSubject = "You recieved a payout!!",

            },
            Items = new List<PayoutItem>()
            {
                new PayoutItem()
                {
                    RecipientType="EMAIL",
                    Amount = new Currency()
                    {
                        CurrencyCode=paymentInfo.CurrencyCode,
                        Value=paymentInfo.Amount.ToString(),
                    },
                    SenderItemId = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                    Receiver=receiverInfo.Email,
                }
            }
        };


        PayoutsPostRequest request = new PayoutsPostRequest();
        request.RequestBody(body);

        try
        {
            var response = await PayPalClient.client().Execute(request);
            var result = response.Result<CreatePayoutResponse>();

            return new PaymentResult() { Successful = true };
        }
        catch(HttpException ex)
        {
            return new PaymentResult() { Successful = false };
        }
    }

}
