namespace Stayin.Payment;

public class OrderCreationResult
{
    public bool Successful { get; set; }
    public string? ApproveLink { get; set; } 
    public string? OrderId { get; set; }

}