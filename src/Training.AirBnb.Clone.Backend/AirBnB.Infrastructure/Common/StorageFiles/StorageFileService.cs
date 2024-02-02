using System.Linq.Expressions;
using AirBnB.Application.Common.StorageFiles;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Infrastructure.Common.StorageFiles;

public class StorageFileService(IStorageFileRepository storageFileRepository) : IStorageFileService
{
    public IQueryable<StorageFile> Get(Expression<Func<StorageFile, bool>>? predicate = default,
        bool asNoTracking = false)
        => storageFileRepository.Get(predicate, asNoTracking);
}