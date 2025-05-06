using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OnlineSinavSistemi.Core.Interfaces
{
    /// <summary>
    /// Generic repository arayüzü
    /// </summary>
    /// <typeparam name="T">Entity türü</typeparam>
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }
} 