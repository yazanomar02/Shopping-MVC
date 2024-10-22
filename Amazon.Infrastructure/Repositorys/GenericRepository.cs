using Amazon.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure.Repositorys
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public readonly AmazonDbContext context;

        public GenericRepository(AmazonDbContext context)
        {
            this.context = context;
        }


        public async Task<T>? Add(T entity)
        {
            var NewEntity = await context.AddAsync(entity);
            return NewEntity.Entity;
        }

        public  T? Get(Guid id)
        {
            return  context.Find<T>(id);
        }

        public IList<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public async Task<int> SaveChanges()
        {
            return await context.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            return context.Update(entity).Entity;
        }
    }
}
