using Ivas.Transactions.Entities.Models;
using Ivas.Transactions.Persistency.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace Ivas.Transactions.Persistency.Context
{
    public class IvasTransactionsDbContext : DbContext, IIvasTransactionsDbContext
    {
        public IvasTransactionsDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
    }
}
