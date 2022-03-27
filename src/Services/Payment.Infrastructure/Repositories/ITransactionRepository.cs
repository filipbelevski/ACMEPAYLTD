using System.Threading.Tasks;
using System;
using Payment.Domain.Model.Transaction;

namespace Payment.Infrastructure.Repositories
{
    public interface ITransactionRepository
    {
        Task<Guid> AuthorizeTransaction(Transaction transaction);

        Task<Transaction> GetTransaction(Guid id);

        Task<Guid> Update(Transaction transaction);
    }
}
