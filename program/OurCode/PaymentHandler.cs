using PayoutsSdk.Payouts;
using PayPalHttp;
using Samples;
using System.Diagnostics;

namespace program;

internal class PaymentHandler : IPaymentHandler
{
    public Task<PaymentResult> GetPaid(UserPaymentInfo senderInfo, PaymentInfo paymentInfo)
    {

        return Task.FromResult(new PaymentResult());
    }

    public async Task<PaymentResult> Pay(UserPaymentInfo receiverInfo, PaymentInfo paymentInfo)
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
