using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreWebPractice
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Commit();   
        Task<int> CommitAsync();
    }
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}