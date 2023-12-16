using AirBnB.Domain.Common;
using AirBnB.Domain.Common.Query;
using Microsoft.EntityFrameworkCore;


namespace AirBnB.Persistence.Extensions;

public static class LinqExtensions
{
    
    public static IQueryable<TSource> ApplySpecification<TSource>(this IQueryable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : class, IEntity
    {
        source = source
            .ApplyPagination(querySpecification)
            .ApplyPredicates(querySpecification)
            .ApplyOrdering(querySpecification)
            .ApplyIncluding(querySpecification);

        return source;
    }
    
    public static IEnumerable<TSource> ApplySpecification<TSource>(this IEnumerable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        source = source
            .ApplyPagination(querySpecification)
            .ApplyPredicates(querySpecification)
            .ApplyOrdering(querySpecification);

        return source;
    }
    
    public static IQueryable<TSource> ApplyPredicates<TSource>(this IQueryable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        querySpecification.FilteringOptions.ForEach(predicate => source = source.Where(predicate));
        return source;
    }

    public static IEnumerable<TSource> ApplyPredicates<TSource>(this IEnumerable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        querySpecification.FilteringOptions.ForEach(predicate => source = source.Where(predicate.Compile()));
        return source;
    }

    public static IQueryable<TSource> ApplyOrdering<TSource>(this IQueryable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        if (!querySpecification.OrderingOptions.Any())
            source.OrderBy(entity => entity.Id);

        querySpecification.OrderingOptions.ForEach(
            orderExpression => source = orderExpression.IsAscending
                ? source.OrderBy(orderExpression.Item1)
                : source.OrderByDescending(orderExpression.Item1)
        );

        return source;
    }

    public static IQueryable<TSource> ApplyIncluding<TSource>(this IQueryable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : class, IEntity
    {
        querySpecification.IncludingOptions.ForEach(includeOption => source = source.Include(includeOption));
        
        return source;
    }
    
    public static IEnumerable<TSource> ApplyOrdering<TSource>(this IEnumerable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        if (querySpecification.OrderingOptions.Count == 0)
            return source.OrderBy(entity => entity.Id);

        querySpecification.OrderingOptions.ForEach(
            orderExpression => source = orderExpression.IsAscending
                ? source.OrderBy(orderExpression.Item1.Compile())
                : source.OrderByDescending(orderExpression.Item1.Compile())
        );

        return source;
    }
    
    public static IQueryable<TSource> ApplyPagination<TSource>(this IQueryable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {   
        return source.Skip((int)((querySpecification.PaginationOptions.PageToken - 1) * querySpecification.PaginationOptions.PageSize))
            .Take((int)querySpecification.PaginationOptions.PageSize);
    }
    
    public static IEnumerable<TSource> ApplyPagination<TSource>(this IEnumerable<TSource> source, QuerySpecification<TSource> querySpecification)
        where TSource : IEntity
    {
        return source.Skip((int)((querySpecification.PaginationOptions.PageToken - 1) * querySpecification.PaginationOptions.PageSize))
            .Take((int)querySpecification.PaginationOptions.PageSize);
    }
}
