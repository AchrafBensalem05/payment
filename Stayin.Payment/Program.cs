using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Orders;
using Stayin.Payment;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IPaymentHandler, PaymentHandler>();


var app = builder.Build();


app.MapGet("/pay/user", async (IPaymentHandler paymentHandler, HttpResponse response) =>
{
    // TODO: get info from request
    var receiverInfo = new UserPaymentInfo()
    {
        //Email = "ac.bensalemaa@gmail.com",
        Email = "sb-474eec25806762@business.example.com",
        UserId = Guid.NewGuid().ToString("N")
    };

    var paymentInfo = new PayUserInfo()
    {
        Amount = 4.2,
        CurrencyCode = "USD",
        PaymentDate = DateTimeOffset.UtcNow,
    };
    
    var result = await paymentHandler.Pay(receiverInfo, paymentInfo);

    await response.WriteAsJsonAsync(result);
});


app.MapGet("/capture/{orderId}", async (HttpContext context, string orderId) =>
{
    var paymentHandler = context.RequestServices.GetRequiredService<IPaymentHandler>();

    var result = await paymentHandler.CapturePaymentOrder(orderId);
    await context.Response.WriteAsJsonAsync(result);
});


app.MapGet("/createPayment", async (HttpContext context, [FromBody] GetPaidInfo paymentInfo) =>
{
    var paymentHandler = context.RequestServices.GetRequiredService<IPaymentHandler>();

    var result = await paymentHandler.CreatePaymentOrder(paymentInfo);


    await context.Response.WriteAsJsonAsync(result);
});

app.Run();
