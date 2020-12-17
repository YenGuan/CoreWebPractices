using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreWebPractice
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        
        #region Create
        void Create(T entity);
        Task CreateAsync(T entity);
        void CreateBatch(IEnumerable<T> entitys);
        #endregion

        #region Read
        IQueryable<T> All();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        /// <summary>取得多筆資料，根據多筆key</summary>
        T Find(params object[] keys);
        T Find(Expression<Func<T, bool>> predicate);
        /// <summary>判斷是否包含該筆資料，根據謂詞篩選</summary>
        bool Contains(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindAllWithInclude(params Expression<Func<T, object>>[] includes);
        IQueryable<T> FindWithRawSql(string query, params object[] parameters);

        #endregion

        #region Update
        void Update(T entity);
        Task UpdateAsync(T entity);
        void UpdateBatch(IEnumerable<T> entitys);
        #endregion

        #region Delete

        void Delete(T entity);
        Task DeleteAsync(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        #endregion

        #region EF Core Async
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindAsync(params object[] keys);
        Task<List<T>> FindAllAsync();
        Task<List<T>> FindAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
          bool enableTracking = true,
          bool ignoreQueryFilters = false);
        #endregion

    }
}
