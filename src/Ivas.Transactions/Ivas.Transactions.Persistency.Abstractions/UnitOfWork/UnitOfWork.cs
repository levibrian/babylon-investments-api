using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ivas.Transactions.Persistency.Abstractions.Repository;
using Ivas.Transactions.Persistency.Abstractions.Repository.Interfaces;
using Ivas.Transactions.Persistency.Abstractions.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ivas.Transactions.Persistency.Abstractions.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, IDisposable
    {
        private Dictionary<(Type type, string name), object> _repositories;

        public TContext Context { get; }

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return (IRepository<TEntity>)GetOrAddRepository(typeof(TEntity), new Repository<TEntity>(Context));
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class
        {
            return (IRepositoryAsync<TEntity>)GetOrAddRepository(typeof(TEntity), new RepositoryAsync<TEntity>(Context));
        }

        public int Commit()
        {
            return Context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        internal object GetOrAddRepository(Type type, object repo)
        {
            if (_repositories == null) _repositories = new Dictionary<(Type type, string Name), object>();

            if (_repositories.TryGetValue((type, repo.GetType().FullName), out var repository)) return repository;

            _repositories.Add((type, repo.GetType().FullName), repo);

            return repo;
        }
    }
}
