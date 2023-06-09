﻿using Stayin.Core;

namespace Stayin.Payment;

/// <summary>
/// The event that will get published when a user gets updated
/// </summary>
public class UserUpdatedEvent : BaseEvent
{
    #region Public Properties

    /// <summary>
    /// The id of the user
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// The username of the user
    /// </summary>
    public required string NewUsername { get; set; }

    /// <summary>
    /// The email of the user
    /// </summary>
    public required string NewEmail { get; set; }

    /// <summary>
    /// The phone number of the user
    /// </summary>
    public string? NewPhoneNumber { get; set; }
    
    #endregion

    /// <inheritdoc/>
    public override async Task Handle(IDataAccess dataAccess)
    {
        await dataAccess.UpdateUserEmail(UserId, NewEmail);
        await dataAccess.SaveChangesAsync();

    }
}
