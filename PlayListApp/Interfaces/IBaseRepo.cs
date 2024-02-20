using PlayListApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlayListApp.Interfaces
{
    public interface IBaseRepo<T> : IDisposable where T : class
    { 
        Task<IEnumerable<T>> GetAll(string[] includes = null);
        Task<IEnumerable<T>> GetById(Expression<Func<T, bool>> expression, string[] includes = null);
        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> expression, string[] includes = null);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
