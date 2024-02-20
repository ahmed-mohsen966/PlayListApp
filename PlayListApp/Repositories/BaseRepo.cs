using PlayListApp.Interfaces;
using PlayListApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace PlayListApp.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T>, IDisposable where T : class
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAll(string[] includes = null)
        {
            IQueryable query = _context.Set<T>();

            if(includes != null)
                foreach (var include in includes)
                {
                    query.Include(include);
                }

            return (IEnumerable<T>)await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetById(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            DbSet<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var includeValue in includes)
                    query = (DbSet<T>)query.Include(includeValue);

            return (IEnumerable<T>)await query.FindAsync(expression);
        }

        public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            DbSet<T> query = _context.Set<T>();

            if(includes != null)
                foreach (var includeValue in includes)
                    query = (DbSet<T>)query.Include(includeValue);

            return (IEnumerable<T>)await query.FirstOrDefaultAsync(expression);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().AddOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}