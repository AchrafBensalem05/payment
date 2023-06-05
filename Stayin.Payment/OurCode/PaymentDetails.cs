namespace Stayin.Payment;

public class PaymentDetails
{
    public required String Id { get; set; }
    public required String UserId { get; set; }
    public required String Email { get; set; }
    public required Double Amount { get; set; }
    public String CurrencyCode { get; set; } = "USD";
}