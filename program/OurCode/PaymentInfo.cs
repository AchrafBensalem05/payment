namespace program;

public class PaymentInfo
{
    public required double Amount { get; set; }
    public required string CurrencyCode { get; set; }
    public required DateTimeOffset PaymentDate { get; set; }

}