using System.Linq.Expressions;

namespace AirBnB.Domain.Comparers;

public class PredicateExpressionComparer<TSource> : IComparer<Expression<Func<TSource, bool>>>
{
    public int Compare(Expression<Func<TSource, bool>>? x, Expression<Func<TSource, bool>>? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;

        return string.Compare(x.ToString(), y.ToString(), StringComparison.Ordinal);
    }
}