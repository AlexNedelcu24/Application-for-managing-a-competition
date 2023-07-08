using System.Collections.Generic;

using model;

namespace persistence
{
public interface ICrudRepository<ID, E> where E : Entity<int>
{
    E Save(E entity);
    E Delete(E entity);

    IEnumerable<E> FindAll();

    E FindOne(ID id);
}
}