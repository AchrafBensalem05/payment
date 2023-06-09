﻿using Microsoft.EntityFrameworkCore;
using Stayin.Core;

namespace Stayin.Payment;

/// <inheritdoc/>
public class DataAccess : IDataAccess
{
    // TODO: try locking the database access,
    // cuz it could be used somewhere else during the same time and cause an exception
    // or maybe not cuz each scope will have his own instance, so no need to lock it
    // will see


    #region Private Members

    /// <summary>
    /// The Db context of our application
    /// </summary>
    private ApplicationDbContext mDbContext;

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="databaseContext">The context of the database to use</param>
    public DataAccess(ApplicationDbContext databaseContext)
    {
        mDbContext = databaseContext;
    }

    #endregion

    #region Interface Implementation

    /// <inheritdoc/>
    public async Task<bool> AddToConsumedEventsIfNotAlreadyAsync(string eventId)
    {
        // Check if the message exists in the database
        var exists = await mDbContext.ConsumedEvents.AnyAsync(x => x.EventId == eventId);

        // If it does exist
        if(exists)
            // Return false
            return false;

        // Otherwise, add it to the database
        mDbContext.ConsumedEvents.Add(new BaseEvent() { EventId = eventId, PublishedTime = DateTimeOffset.UtcNow });

        // Save the changes
        await mDbContext.SaveChangesAsync();

        // return true
        return true;
    }

    /// <inheritdoc/>
    public Task CreateEventAsync(BaseEvent newEvent)
     => Task.FromResult(mDbContext.ConsumedEvents.Add(newEvent));

    /// <inheritdoc/>
    public Task<bool> EnsureCreatedAsync() => mDbContext.Database.EnsureCreatedAsync();

    /// <inheritdoc/>
    public Task<List<string>> LoadExistingEventIdsAsync()
        => mDbContext.ConsumedEvents.Select(x => x.EventId).ToListAsync();

    /// <inheritdoc/>
    public Task SaveChangesAsync() => mDbContext.SaveChangesAsync();

    public Task<List<(ApplicationUser User, List<ApplicationRole> Roles, int ReservationsCount, int PublicationsCount)>> GetUsersInPage(int page, int size)
    {
        throw new NotImplementedException();
    }

    public Task<(ApplicationUser? User, List<ApplicationRole>? Roles, int ReservationsCount, int PublicationsCount)> GetUserDetails(string username)
    {
        throw new NotImplementedException();
    }

    public Task AddUserPaymentDetails(PaymentDetails details)
    {
        mDbContext.PaymentDetailsDb.Add(details);
        return Task.CompletedTask;
    }

    public Task UpdateUserEmail(string userId, string email)
    {
        var userDetails = mDbContext.PaymentDetailsDb.Where(x => x.UserId == userId).First();
        userDetails.Email = email;
        return Task.CompletedTask;
    }

    public Task DeleteUserDetails(string userId)
    {
        var toDelete = mDbContext.PaymentDetailsDb.First(x => x.UserId == userId);

        mDbContext.PaymentDetailsDb.Remove(toDelete);

        return Task.CompletedTask;
    }

    public Task AddRentalAsync(Rental rental)
    {
        throw new NotImplementedException();
    }

    public Task CreateHousePublicationAsync(HousePublication housePublication)
    {
        throw new NotImplementedException();
    }

    public Task<HousePublication?> GetHousePublicationById(string id)
    {
        throw new NotImplementedException();
    }

    #endregion
}
