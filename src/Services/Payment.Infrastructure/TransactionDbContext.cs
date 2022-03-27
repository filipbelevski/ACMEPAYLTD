using Microsoft.EntityFrameworkCore;
using Payment.Domain.Model.Transaction;
using Payment.Infrastructure.EntityConfigurations;
using System.Data;

namespace Payment.Infrastructure
{
    public class TransactionDbContext : DbContext, ITransactionDbContext
    {
        private const string TransactionSchema = "Transaction";

        public TransactionDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        public IDbConnection Connection => Database.GetDbConnection();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TransactionConfiguration(TransactionSchema));

            modelBuilder.HasDefaultSchema(TransactionSchema);
        }
    }
}
