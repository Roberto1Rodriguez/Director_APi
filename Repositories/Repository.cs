using DirectorAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace DirectorAPI.Repositories
{
    public class Repository<T> where T: class
    {
        public Repository(Sistem21PrimariaContext context)
        {
            Context = context;
        }
        public Sistem21PrimariaContext Context { get; }

        public DbSet<T> GetAll()
        {
            return Context.Set<T>();
        }
        public T? Get(object id)
        {
            return Context.Find<T>(id);
        }
        public void Insert(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }
        public void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }
        public void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }
    }
}
