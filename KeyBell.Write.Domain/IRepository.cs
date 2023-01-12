namespace KeyBell.Write.Domain;

public interface IRepository<TAggregateRoot, TId>
    where TAggregateRoot : AggregateRoot<TId>
    where TId : notnull
{
}