using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Repository interface for managing storage files.
/// </summary>
public interface IStorageFileRepository
{
    /// <summary>
    /// Retrieves a queryable collection of StorageFile entities based on the specified predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the StorageFile entities (optional).</param>
    /// <param name="asNoTracking">Indicates whether to disable change tracking for the entities (default: false).</param>
    /// <returns>A queryable collection of StorageFile entities.</returns>
    IQueryable<StorageFile> Get(Expression<Func<StorageFile, bool>>? predicate = default, bool asNoTracking = false);
}