using Ivas.Transactions.Persistency.Entities;
using Ivas.Transactions.Persistency.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace Ivas.Transactions.Persistency.Context
{
    public class TransactionsDbContext : DbContext, ITransactionsDbContext
    {
        public TransactionsDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<TransactionEntity> Transactions { get; set; }
        
        public virtual DbSet<TransactionTypeEntity> TransactionTypes { get; set; }
    }
}
