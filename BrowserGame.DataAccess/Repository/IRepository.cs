using BrowserGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BrowserGame.DataAccess.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int offset = 0, int limit = 0);
        int Count(Expression<Func<T, bool>>? filter = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        T Get(int id, string? includeProperties = null, bool tracked = false);
        void Add(T entity, bool save = false);
        void Update(T entity, bool save = false);
        void Detach(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        void Save();
        bool IsStored(int id);
    }
}
