using AirBnB.Application.Common.StorageFiles;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Infrastructure.Common.StorageFiles;

public class StorageFileService(IStorageFileRepository storageFileRepository) : IStorageFileService
{
    public ValueTask<IList<StorageFile>> GetAsync(QuerySpecification<StorageFile> querySpecification, CancellationToken cancellationToken = default)
        => storageFileRepository.GetAsync(querySpecification, cancellationToken);

    public ValueTask<IList<StorageFile>> GetAsync(QuerySpecification querySpecification, CancellationToken cancellationToken = default)
        => storageFileRepository.GetAsync(querySpecification, cancellationToken);
}