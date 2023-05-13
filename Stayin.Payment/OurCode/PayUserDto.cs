namespace Stayin.Payment;

public class PayUserDto
{
    public required UserPaymentInfo UserPaymentInfo { get; set; }
    public required PayUserInfo PayUserInfo { get; set; }
}
