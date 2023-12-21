using System.Linq.Expressions;

namespace AirBnB.Domain.Comparers;

/// <summary>
/// Represents a comparer for comparing and sorting expressions of type <see cref="Expression{TDelegate}"/> that represent
/// predicate functions in a source data type.
/// </summary>
/// <typeparam name="TSource">The type of the source data.</typeparam>
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