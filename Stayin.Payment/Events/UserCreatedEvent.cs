using Stayin.Core;

namespace Stayin.Payment;

/// <summary>
/// The event that will get published when a user got created
/// </summary>
public class UserCreatedEvent : BaseEvent
{
    #region Public Properties

    /// <summary>
    /// The id of the created user
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// The username of the user
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// The email of the user
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// The phone number of the user
    /// </summary>
    public string? PhoneNumber { get; set; }

    #endregion

    /// <inheritdoc/>
    public override async Task Handle(IDataAccess dataAccess)
    {
        var details = new PaymentDetails() { Amount = 0, Email = Email, Id = Guid.NewGuid().ToString("N"), UserId = UserId };
        await dataAccess.AddUserPaymentDetails(details);
        await dataAccess.SaveChangesAsync();
    }
}
