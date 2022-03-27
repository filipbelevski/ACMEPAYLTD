using Payment.Domain.Model.Transaction.Enum;
using System;
using System.Text.Json.Serialization;

namespace Payment.API.Application.Commands.Void
{
    public class VoidResponse
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionStatus Status{ get; set; }
    }
}
