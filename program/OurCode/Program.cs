using PayPalCheckoutSdk.Orders;
using program;
using Samples;
using Samples.AuthorizeIntentExamples;


var result = await CreateOrderSample.CreateOrderWithMinimumFields(true);


var order= result.Result<Order>();

var newResult = await AuthorizeOrderSample.AuthorizeOrder(order.Id);
order = newResult.Result<Order>();


Console.ReadLine();


//var handler = new PaymentHandler();
//var receiverInfo = new UserPaymentInfo()
//{
//    //Email = "ac.bensalemaa@gmail.com",
//    Email = "sb-474eec25806762@business.example.com",
//    Id = Guid.NewGuid().ToString("N")
//};
//var paymentInfo = new PaymentInfo() { Amount = 4.2 , CurrencyCode="USD", PaymentDate = DateTimeOffset.UtcNow };
//var result = await handler.Pay(receiverInfo, paymentInfo);
