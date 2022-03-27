
using MediatR;
using Payment.API.Common.Contracts.Responses;
using Payment.Infrastructure;
using System.Linq;
using Dapper;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Payment.Domain.Model.Transaction;
using Payment.API.Common.StringExtensions;

namespace Payment.API.Application.Queries.Transactions.List
{
    public class TransactionsListQueryHandler : IRequestHandler<TransactionsListQuery, ListResultDto<TransactionsListQueryResponse>>
    {
        private readonly TransactionDbContext transactionDbContext;

        public TransactionsListQueryHandler(TransactionDbContext transactionDbContext)
        {
            this.transactionDbContext = transactionDbContext;
        }

        public async Task<ListResultDto<TransactionsListQueryResponse>> Handle(TransactionsListQuery request, CancellationToken cancellationToken)
        {
            var result = new ListResultDto<TransactionsListQueryResponse>();

            string searchFilter = string.IsNullOrEmpty(request.SearchString)
                ? string.Empty :
@"AND (t.[UID] LIKE @SearchString
OR t.[Amount] LIKE @SearchString
OR t.[CardholderNumber] LIKE @SearchString
OR t.[OrderReference] LIKE @SearchString)";

            string statusFilter = request.Status == null ?
                string.Empty :
@"AND t.[TransactionStatus] = @Status";

            string dateFromFilter = request.From == null ?
                string.Empty :
@"AND t.[CreatedOn] >= @DateFrom";

            string dateToFilter = request.To == null ?
                string.Empty :
@"AND t.[CreatedOn] <= @DateTo";



            string query = $@"
SELECT t.[Uid], t.Amount, t.Currency, t.CardholderNumber, t.HolderName, t.OrderReference, t.TransactionStatus as Status, t.CreatedOn as CreatedOn
FROM [Transaction].[tblTransactions] t
WHERE t.[DeletedOn] is null
{searchFilter}
{statusFilter}
{dateFromFilter}
{dateToFilter}
ORDER BY t.[ID]
OFFSET @Skip ROWS
FETCH NEXT @Take ROWS ONLY";
            string countQuery = $@"
SELECT COUNT(*) as Count
FROM [Transaction].[tblTransactions] t
WHERE t.[DeletedOn] is null
{searchFilter}
{statusFilter}
{dateFromFilter}
{dateToFilter}";

            string marsQuery = $"{query}{countQuery}";


            using (var connection = transactionDbContext.Connection)
            {
                using (var multi = await connection.QueryMultipleAsync(marsQuery, new
                {
                    Skip = (request.CurrentPage - 1) * request.PageSize,
                    Take = request.PageSize,
                    SearchString = request.SearchString,
                    Status = request.Status,
                    DateFrom = request.From,
                    DateTo = request.To
                }))
                {
                    result.List = multi.Read<Transaction>()
                        .Select(x => MapToTransactionListQueryResponse(x))
                        .ToList();
                    result.TotalCount = multi.Read<int>().Single();
                    result.CurrentPage = request.CurrentPage;
                    result.PageSize = request.PageSize;
                }

                return result;
            }
        }
        public TransactionsListQueryResponse MapToTransactionListQueryResponse(Transaction transaction)
        {
            return new TransactionsListQueryResponse
            {
                PaymentId = transaction.Uid,
                Amount = transaction.Amount,
                CardholderNumber = transaction.CardholderNumber.AnonymizeCardholderNumberString(),
                CreatedOn = transaction.CreatedOn,
                Currency = transaction.Currency,
                HolderName = transaction.HolderName,
                OrderReference = transaction.OrderReference,
                Status = transaction.Status
            };
        }

    }
}
