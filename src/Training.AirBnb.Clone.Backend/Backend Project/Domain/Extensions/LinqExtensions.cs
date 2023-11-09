namespace Backend_Project.Domain.Extensions;

public static class LinqExtensions
{
    public static (IEnumerable<TSource> addedItems, IEnumerable<TSource> removedItems) GetAddedAndRemovedItems<TSource>
        (this IEnumerable<TSource> oldCollection, IEnumerable<TSource> updatedCollection) where TSource : notnull
    {
        var addedItems = updatedCollection.Except(oldCollection);
        var removedItems = oldCollection.Except(updatedCollection);

        return (addedItems, removedItems);
    }

    public static (IEnumerable<TSource> addedItems, IEnumerable<TSource> removedItems) GetAddedAndRemovedItemsBy<TSource, TKey>
        (this IEnumerable<TSource> oldCollection, IEnumerable<TSource> updatedCollection, Func<TSource, TKey> keySelector) where TSource : notnull
    {
        var updatedSet = new HashSet<TKey>(updatedCollection.Select(keySelector));
        var removedItems = oldCollection.Where(item => !updatedSet.Contains(keySelector(item)));

        var oldSet = new HashSet<TKey>(oldCollection.Select(keySelector));
        var addedItems = updatedCollection.Where(item => !oldSet.Contains(keySelector(item)));

        return (addedItems, removedItems);
    }
}