using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Payment.Domain.Model.Transaction;
using System;
using System.Threading.Tasks;

namespace Payment.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext dbContext;

        public TransactionRepository(TransactionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Transaction> GetTransaction(Guid id)
        {
            return await dbContext.Transactions.SingleOrDefaultAsync(x => x.Uid == id);
        }

        public async Task<Guid> Update(Transaction transaction)
        {
            dbContext.Entry(transaction).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();

            return dbContext.Entry(transaction).Entity.Uid;
        }

        public async Task<Guid> AuthorizeTransaction(Transaction transaction)
        {
            transaction.Uid = Guid.NewGuid();
            transaction.CreatedOn = DateTime.Now;

            EntityEntry<Transaction> entry = await dbContext.Transactions.AddAsync(transaction);

            await dbContext.SaveChangesAsync();

            return entry.Entity.Uid;
        }
    }
}
