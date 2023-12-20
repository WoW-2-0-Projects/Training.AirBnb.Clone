using System.Linq.Expressions;

namespace AirBnB.Domain.Comparers;

public class OrderExpressionComparer<TSource> : IComparer<(Expression<Func<TSource, object>> KeySelector, bool IsAscending)>
{
    public int Compare(
        (Expression<Func<TSource, object>> KeySelector, bool IsAscending) x, 
        (Expression<Func<TSource, object>> KeySelector, bool IsAscending) y)
    {
        if (ReferenceEquals(x.KeySelector, y.KeySelector)) return 0;
        if (ReferenceEquals(null, y.KeySelector)) return 1;
        if (ReferenceEquals(null, x.KeySelector)) return -1;

        var keySelectorComparison = string.Compare(x.KeySelector.ToString(), y.KeySelector.ToString(), StringComparison.Ordinal);

        return keySelectorComparison != 0 ? keySelectorComparison : Comparer<bool>.Default.Compare(x.IsAscending, y.IsAscending);
    }
}