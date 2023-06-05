using Stayin.Core;

namespace Stayin.Payment;

public class UserDeletedEvent : BaseEvent
{
    #region Public Properties

    /// <summary>
    /// The id of the deleted user
    /// </summary>
    public required string UserId { get; set; }

    #endregion

    /// <inheritdoc/>
    public override async Task Handle(IDataAccess dataAccess)
    {
        await dataAccess.DeleteUserDetails(UserId);
        await dataAccess.SaveChangesAsync();
    }
}
