namespace Stayin.Payment;

public class PayUserInfo
{
    public required double Amount { get; set; }
    public required string CurrencyCode { get; set; }
    public required DateTimeOffset PaymentDate { get; set; }

}