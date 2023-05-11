namespace Stayin.Payment;

public interface IPaymentHandler
{
    /// <summary>
    /// Pay a specific user from the admin account using it's info
    /// </summary>
    /// <param name="receiverInfo">Information about the user to pay</param>
    /// <param name="paymentInfo">Information about the payment to apply</param>
    /// <returns></returns>
    Task<PaymentResult> Pay(UserPaymentInfo receiverInfo, PayUserInfo paymentInfo);

    Task<OrderCreationResult> CreatePaymentOrder(GetPaidInfo paymentInfo);


    Task<OrderCaptureResult> CapturePaymentOrder(string orderId);

}
