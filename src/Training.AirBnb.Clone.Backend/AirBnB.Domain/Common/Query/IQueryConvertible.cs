namespace AirBnB.Domain.Common.Query;

public interface IQueryConvertible<TEntity> where TEntity : IEntity
{
    QuerySpecification<TEntity> ToQuerySpecification();
}