using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure.Repositorys
{
    public interface IRepository<T>
    {
        Task<T>? Add(T entity);
        T Update(T entity);
        T? Get(Guid id);
        IList<T> GetAll();
        Task<int> SaveChanges();
    }
}
