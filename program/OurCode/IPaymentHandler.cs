namespace program;

public interface IPaymentHandler
{
    /// <summary>
    /// Pay a specific user from the admin account using it's info
    /// </summary>
    /// <param name="receiverInfo">Information about the user to pay</param>
    /// <param name="paymentInfo">Information about the payment to apply</param>
    /// <returns></returns>
    Task<PaymentResult> Pay(UserPaymentInfo receiverInfo, PaymentInfo paymentInfo);

    Task<PaymentResult> GetPaid(UserPaymentInfo senderInfo, PaymentInfo paymentInfo);

}
