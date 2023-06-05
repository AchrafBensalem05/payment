
using Microsoft.EntityFrameworkCore;
using Stayin.Core;

namespace Stayin.Payment;

/// <summary>
/// The data context for the application database
/// </summary>
public class ApplicationDbContext : DbContext
{
    #region Public Properties

    /// <summary>
    /// A table that will contain all the events that have been consumed by our application
    /// </summary>
    public DbSet<BaseEvent> ConsumedEvents { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="options">The options to configure the database context</param>
    public ApplicationDbContext(DbContextOptions options) :base(options){}

    #endregion

  
}
