using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cqrs.Domain.Interfaces
{
    public interface IBaseRepository<Entity>
    {
        abstract Task<int> Create(Entity entity);
        abstract Task<IEnumerable<Entity>> GetAll();

    }
}
