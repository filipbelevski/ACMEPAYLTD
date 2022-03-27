using Microsoft.EntityFrameworkCore;
using Payment.Domain.Model.Transaction;
using System.Data;

namespace Payment.Infrastructure
{
    public interface ITransactionDbContext
    {
        DbSet<Transaction> Transactions { get; set; }

        IDbConnection Connection { get; }
    }
}