using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

public interface IStorageFileRepository
{
    ValueTask<IList<StorageFile>> GetAsync(QuerySpecification<StorageFile> querySpecification, CancellationToken cancellationToken = default);

    ValueTask<IList<StorageFile>> GetAsync(QuerySpecification querySpecification, CancellationToken cancellationToken = default);
}