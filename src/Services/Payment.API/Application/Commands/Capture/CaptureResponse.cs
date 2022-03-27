using System.Text.Json.Serialization;
using System;
using Payment.Domain.Model.Transaction.Enum;

namespace Payment.API.Application.Commands.Capture
{
    public class CaptureResponse
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionStatus Status { get; set; }
    }
}
