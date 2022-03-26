using MediatR;
using Payment.API.Common.Contracts.Responses;
using Payment.Domain.Model.Transaction.Enum;
using System;

namespace Payment.API.Application.Queries.Transactions.List
{
    public class TransactionsListQuery : IRequest<ListResultDto<TransactionsListQueryResponse>>
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public string SearchString { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public TransactionStatus? Status { get; set; }

    }
}
