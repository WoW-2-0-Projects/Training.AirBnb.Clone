using System.Linq.Expressions;
using AirBnB.Domain.Common.Caching;
using AirBnB.Domain.Comparers;
using Microsoft.EntityFrameworkCore.Query;

namespace AirBnB.Domain.Common.Query;

public class QuerySpecification<TEntity>(uint pageSize, uint pageToken, bool asNoTracking) : CacheModel where TEntity : IEntity
{
    public List<Expression<Func<TEntity, bool>>> FilteringOptions { get; } = [];

    public List<(Expression<Func<TEntity, object>> KeySelector, bool IsAscending)> OrderingOptions { get; } = [];

    public List<Expression<Func<TEntity, object>>> IncludingOptions { get; }= [];
    
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