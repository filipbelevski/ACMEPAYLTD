using Payment.Domain.Model.Transaction.Enum;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Payment.API.Application.Queries.Transactions.List
{
    public class TransactionsListQueryResponse
    {
        //comment
        public Guid PaymentId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string CardholderNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public string HolderName { get; set; }

        public string OrderReference { get ; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionStatus Status { get; set; }
    }
}
