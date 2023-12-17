using System.Linq.Expressions;
using AirBnB.Domain.Common.Caching;
using AirBnB.Domain.Comparers;
using Microsoft.EntityFrameworkCore.Query;

namespace AirBnB.Domain.Common.Query;

/// <summary>
/// Represents a query specification for retrieving entities from a cache.
/// </summary>
/// <param name="pageSize"></param>
/// <param name="pageToken"></param>
/// <param name="asNoTracking"></param>
/// <typeparam name="TEntity"></typeparam>
public class QuerySpecification<TEntity>(uint pageSize, uint pageToken, bool asNoTracking) : CacheModel where TEntity : IEntity
{
    /// <summary>
    /// Gets filtering options collection for query.
    /// </summary>
    public List<Expression<Func<TEntity, bool>>> FilteringOptions { get; } = [];

    /// <summary>
    /// Gets ordering options collection for query.
    /// </summary>
    public List<(Expression<Func<TEntity, object>> KeySelector, bool IsAscending)> OrderingOptions { get; } = [];

    /// <summary>
    /// /// Gets including options collection for query.
    /// </summary>
    public List<Expression<Func<TEntity, object>>> IncludingOptions { get; }= [];
    
    /// <summary>
    /// /// Gets pagination options for query.
    /// </summary>
    public FilterPagination PaginationOptions { get; set; } = new(pageSize, pageToken);

    public bool AsNoTracking { get; } = asNoTracking;
    
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        var expressionEqualityComparer = ExpressionEqualityComparer.Instance;

        foreach (var filter in FilteringOptions.Order(new PredicateExpressionComparer<TEntity>()))
            hashCode.Add(expressionEqualityComparer.GetHashCode(filter));

        foreach (var filter in OrderingOptions)
            hashCode.Add(expressionEqualityComparer.GetHashCode(filter.KeySelector));
        
        hashCode.Add(PaginationOptions);

        return hashCode.ToHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is QuerySpecification<TEntity> querySpecification && querySpecification.GetHashCode() == GetHashCode();
    }

    public override string CacheKey => $"{typeof(TEntity).Name}_{GetHashCode()}";
}