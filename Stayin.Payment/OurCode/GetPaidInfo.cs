namespace Stayin.Payment;

public class GetPaidInfo
{
    public required double Amount { get; set; }
    public required string CurrencyCode { get; set; }
    public required DateTimeOffset PaymentDate { get; set; }
    public required string ReservationId { get; set; }

    /// <summary>
    /// Path to take user to when he cancels payment
    /// </summary>
    public required string CancelUrl { get; set; }

    /// <summary>
    /// Url to take user to when payment is approved
    /// </summary>
    public required string ReturnUrl { get; set; }

}