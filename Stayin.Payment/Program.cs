using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Stayin.Payment;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Stayin.Core;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IPaymentHandler, PaymentHandler>();

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Use SQL server as backend database
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
    
});

// Add data access service
builder.Services.AddScoped<IDataAccess, DataAccess>();
// Add queue consumer service
builder.Services.AddHostedService<QueueConsumerService>();

// Add RabbitMQ event bus
builder.Services.AddTransient<IEventBus, RabbitMQEventBus>(provider => new RabbitMQEventBus(
    provider.GetRequiredService<IConfiguration>()["RabbitMQ:Queue"]!,
    provider.GetRequiredService<IConfiguration>()["RabbitMQ:Uri"]!,
    provider.GetRequiredService<ILogger<RabbitMQEventBus>>()));

var app = builder.Build();



// Make sure that the database exists
using var scope = app.Services.CreateScope();
var newlyCreated = await scope.ServiceProvider.GetRequiredService<IDataAccess>().EnsureCreatedAsync();


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
app.MapGet("/", 
    () => "hello World");
app.MapGet("/pay/user", async (IPaymentHandler paymentHandler, HttpResponse response, [FromBody] PayUserDto paymentDetails) => {
    var result = await paymentHandler.Pay(paymentDetails.UserPaymentInfo, paymentDetails.PayUserInfo);
    
    await response.WriteAsJsonAsync(result);
});


app.MapGet("/capture/{orderId}", async (HttpContext context, string orderId,IEventBus eventBus) =>
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
