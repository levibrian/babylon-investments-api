using System;
using System.Threading.Tasks;
using Ivas.Transactions.Persistency.Abstractions.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ivas.Transactions.Persistency.Abstractions.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class;

        int Commit();

        Task<int> CommitAsync();
    }

    public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
