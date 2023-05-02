using PayPalCheckoutSdk.Orders;
using program;
using Samples;
using Samples.AuthorizeIntentExamples;
using Samples.CaptureIntentExamples;





// Create order
var result = await CreateOrderSample.CreateOrderWithMinimumFields(true);

// Ask user to approve through approve link
var order = result.Result<Order>();
var approveLink =  order.Links.Where(x => x.Rel == "approve").First();

// display page for approval
 Console.WriteLine(approveLink.Href);

// If user approves
// Capture order
var res = await CaptureOrderSample.CaptureOrder(order.Id);





//var newResult = await AuthorizeOrderSample.AuthorizeOrder(order.Id);
//order = newResult.Result<Order>();

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


