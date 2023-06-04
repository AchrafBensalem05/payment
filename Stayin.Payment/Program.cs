using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Stayin.Payment;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IPaymentHandler, PaymentHandler>();


var app = builder.Build();



//{
//    "UserPaymentInfo": {
//        "Email": "sb-474eec25806762@business.example.com",
//        "UserId": "1"
//    },
//    "PayUserInfo":{
//        "Amount" : 999,
//        "CurrencyCode" : "USD",
//        "PaymentDate": "2019-07-26T16:59:57-05:00",
//    }
//}

app.MapGet("/pay/user", async (IPaymentHandler paymentHandler, HttpResponse response, [FromBody] PayUserDto paymentDetails) => {
    var result = await paymentHandler.Pay(paymentDetails.UserPaymentInfo, paymentDetails.PayUserInfo);

    await response.WriteAsJsonAsync(result);
});


app.MapGet("/capture/{orderId}", async (HttpContext context, string orderId) =>
{
    var paymentHandler = context.RequestServices.GetRequiredService<IPaymentHandler>();

    var result = await paymentHandler.CapturePaymentOrder(orderId);
    await context.Response.WriteAsJsonAsync(result);
});



//{
//    "Amount": 50,
//    "CurrencyCode": "USD",
//    "PaymentDate": "2019-07-26T16:59:57-05:00",
//    "ReservationId": "33",
//    "CancelUrl": "https://youtube.com",
//    "ReturnUrl": "https://github.com"
//}

app.MapPost("/create/order", async (HttpContext context, [FromBody] GetPaidInfo paymentInfo) =>
{

    var paymentHandler = context.RequestServices.GetRequiredService<IPaymentHandler>();

    var result = await paymentHandler.CreatePaymentOrder(paymentInfo);
    
    await context.Response.WriteAsJsonAsync(result);
});

app.Run();
