using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace CoreWebPractice
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }
        #region DbContext

        public readonly DbContext _dbContext;
        public readonly DbSet<T> _dbSet;

        #endregion

        #region Create
        public void Create(T entity)
        {
            this._dbSet.Add(entity);
            this._dbContext.SaveChanges();
        }
        public async Task CreateAsync(T entity)
        {
            this._dbSet.Add(entity);
            await this._dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 批次新增
        /// </summary>
        /// <param name="entitys">entitys</param>
        public void CreateBatch(IEnumerable<T> entitys)
        {
            this._dbSet.AddRange(entitys);
            this._dbContext.SaveChanges();
        }

        #endregion

        #region Read

        public virtual IQueryable<T> All()
        {
            return this._dbSet.AsQueryable();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.Where(predicate);
        }

        /// <summary>取得多筆資料，根據多筆key</summary>
        public virtual T Find(params object[] keys)
        {
            return this._dbSet.Find(keys);
        }
        public async Task<List<T>> FindAllAsync()
        {
            return await this._dbSet.ToListAsync();
        }
        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.FirstOrDefault(predicate);
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await this._dbSet.FirstOrDefaultAsync(predicate);
        }
  
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool enableTracking = true,
         bool ignoreQueryFilters = false)
        {
            IQueryable<T> query = _dbSet;

            if (!enableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

            return orderBy != null ? orderBy(query).FirstOrDefaultAsync() : query.FirstOrDefaultAsync();
        }
        public async Task<T> FindAsync(params object[] keys)
        {
            return await this._dbSet.FindAsync(keys);
        }
        public async Task<List<T>> FindAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = this._dbSet.AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return await query.ToListAsync();

        }
        public async Task<T> FindAllWithIncludeAsync(Expression<Func<T, bool>> predicate)
        {
            return await this._dbSet.FirstOrDefaultAsync(predicate);

        }
        /// <summary>判斷是否包含該筆資料，根據謂詞篩選</summary>
        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.Count(predicate) > 0; ;
        }

        public IQueryable<T> FindAllWithInclude(params Expression<Func<T, object>>[] includes)
        {
            var query = this._dbSet.AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));
            }
            return query;
        }

        /// <summary>取得資料筆數</summary>
        public int Count
        {
            get { return this._dbSet.Count(); }
        }

        public IQueryable<T> FindWithRawSql(string query, params object[] parameters)
        {
            return this._dbSet.FromSqlRaw(query, parameters).AsQueryable();
        }

        #endregion

        #region Update
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            this._dbContext.Entry(entity).State = EntityState.Modified;
            this._dbContext.SaveChanges();
          
        }
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            this._dbContext.Entry(entity).State = EntityState.Modified;
            await this._dbContext.SaveChangesAsync();

        }
        public void UpdateBatch(IEnumerable<T> entitys)
        {
            foreach (var entity in entitys)
            {
                _dbSet.Attach(entity);
                this._dbContext.Entry(entity).State = EntityState.Modified;
            }
            this._dbContext.SaveChanges();

        }
        #endregion

        #region Delete

        public void Delete(T entity)
        {
            this._dbSet.Attach(entity);
            this._dbSet.Remove(entity);
            this._dbContext.SaveChanges();
        }
        public async Task DeleteAsync(T entity)
        {
            this._dbSet.Attach(entity);
            this._dbSet.Remove(entity);
            await this._dbContext.SaveChangesAsync();
        }
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var ToDeleteItems = Filter(predicate);
            foreach (var obj in ToDeleteItems)
            {
                this._dbSet.Remove(obj);
            }
            this._dbContext.SaveChanges();
        }
      
        #endregion

        #region Other

        private IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return this._dbSet.Where(predicate).AsQueryable<T>();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await this._dbSet.FindAsync(predicate);
        }
        #endregion

    }
}