using Payment.Domain.Entities;
using Payment.Domain.Model.Transaction.Enum;

namespace Payment.Domain.Model.Transaction
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string CardholderNumber { get; set; }

        public string HolderName { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public int CVV { get; set; }

        public string OrderReference { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
