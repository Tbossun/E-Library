﻿using System.Linq.Expressions;

namespace E_Library.Data.Repositories.IRepositories
{
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string includeProperties = null, bool tracked = false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetPageAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();

    }
}
