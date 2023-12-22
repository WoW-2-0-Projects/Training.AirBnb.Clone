using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Identity.StorageFiles;

public interface IStorageFileService
{
    ValueTask<IList<StorageFile>> GetAsync(QuerySpecification<StorageFile> querySpecification, CancellationToken cancellationToken = default);

    ValueTask<IList<StorageFile>> GetAsync(QuerySpecification querySpecification, CancellationToken cancellationToken = default);
}