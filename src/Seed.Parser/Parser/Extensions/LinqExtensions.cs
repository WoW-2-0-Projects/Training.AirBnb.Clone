namespace Parser.Extensions;

public static class LinqExtensions
{
    public static IEnumerable<(T element, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }


}