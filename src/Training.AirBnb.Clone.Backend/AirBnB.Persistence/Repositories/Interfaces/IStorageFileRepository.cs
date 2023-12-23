using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Repository interface for managing storage files.
/// </summary>
public interface IStorageFileRepository
{
    /// <summary>
    /// Retrieves a list of storage files based on the provided query specification.
    /// </summary>
    /// <param name="querySpecification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<IList<StorageFile>> GetAsync(QuerySpecification<StorageFile> querySpecification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of storage files based on the provided query specification.
    /// </summary>
    /// <param name="querySpecification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<IList<StorageFile>> GetAsync(QuerySpecification querySpecification, CancellationToken cancellationToken = default);
}