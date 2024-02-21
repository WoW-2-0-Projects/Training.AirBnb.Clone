using AirBnB.Domain.Common;
using AirBnB.Domain.Common.Entities;
using AirBnB.Domain.Common.Query;
using Microsoft.EntityFrameworkCore;


namespace AirBnB.Persistence.Extensions;

/// <summary>
/// Provides extension methods for applying query specifications to IQueryable and IEnumerable collections.
/// </summary>
public static class LinqExtensions
{
    public static IQueryable<TSource> ApplyPagination<TSource>(this IQueryable<TSource> sources,
        FilterPagination paginationOptions)
    {
        return sources.Skip((int)((paginationOptions.PageToken - 1) * paginationOptions.PageSize))
            .Take((int)paginationOptions.PageSize);
    }
    
    public static async Task<T?> FirstOrDefaultAsync<T>(
        this IEnumerable<T> source,
        Func<T, Task<bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);

        if(cancellationToken.IsCancellationRequested) return default;
        
        foreach (var item in source)
            if (await predicate(item))
                return item;

        return default;
    }
}
