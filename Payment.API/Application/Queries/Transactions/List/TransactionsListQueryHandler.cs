
using MediatR;
using Payment.API.Common.Contracts.Responses;
using Payment.Infrastructure;
using System.Linq;
using Dapper;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System;
using Payment.Domain.Model.Transaction;

namespace Payment.API.Application.Queries.Transactions.List
{
    public class TransactionsListQueryHandler : IRequestHandler<TransactionsListQuery, ListResultDto<TransactionsListQueryResponse>>
    {
        private readonly TransactionDbContext transactionDbContext;
        private readonly IMapper mapper;

        public TransactionsListQueryHandler(TransactionDbContext transactionDbContext, IMapper mapper)
        {
            this.transactionDbContext = transactionDbContext;
            this.mapper = mapper;
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

            string statusFilter = request.Status == null ? string.Empty :
@"AND t.[TransactionStatus] = @Status";


            string query = $@"
SELECT t.[Uid], t.Amount, t.Currency, t.CardholderNumber, t.HolderName, t.OrderReference, t.TransactionStatus as Status
FROM [Transaction].[tblTransactions] t
WHERE t.[DeletedOn] is null
{searchFilter}
{statusFilter}
ORDER BY t.[ID]
OFFSET @Skip ROWS
FETCH NEXT @Take ROWS ONLY";
            string countQuery = $@"
SELECT COUNT(*) as Count
FROM [Transaction].[tblTransactions] t
WHERE t.[DeletedOn] is null
{searchFilter}
{statusFilter}";

            string marsQuery = $"{query}{countQuery}";


            using (var connection = transactionDbContext.Connection)
            {
                using (var multi = await connection.QueryMultipleAsync(marsQuery, new
                {
                    Skip = (request.CurrentPage - 1) * request.PageSize,
                    Take = request.PageSize,
                    SearchString = request.SearchString,
                    Status = request.Status
                }))
                {
                    result.List = multi.Read<Transaction>()
                        .Select(x => mapper.Map<TransactionsListQueryResponse>(x))
                        .ToList();
                    result.TotalCount = multi.Read<int>().Single();
                    result.CurrentPage = request.CurrentPage;
                    result.PageSize = request.PageSize;
                }


                return result;

                //var result = await connection.QueryMultipleAsync<Transaction>(query, new
                //{
                //    Skip = (request.CurrentPage - 1) * request.PageSize,
                //    Take = request.PageSize,
                //    SearchString = request.SearchString,
                //    Status = request.Status
                //});

                //if(result == null)
                //{
                //    throw new SystemException("Something went wrong.");
                //}

                //return new ListResultDto<TransactionsListQueryResponse>
                //{
                //    CurrentPage = request.CurrentPage,
                //    List = result.Select(x => mapper.Map<TransactionsListQueryResponse>(x)).ToList(),
                //    PageSize = request.PageSize,
                //    TotalCount = result.Count()
                //};
            }
        }

        //private List<TransactionsListQueryResponse> MapToTransactionListQueryResponse(IEnumerable<dynamic> listResult)
        //{
        //    return listResult.Select(result => new TransactionsListQueryResponse
        //    {
        //        PaymentId = new Guid(result.Uid),
        //        Amount = (decimal)result.Amount,
        //        Currency = result.Currency,
        //        CardholderNumber = result.CardholderNumber,
        //        CreatedOn = result.CreatedOn,
        //        HolderName = result.HolderName,
        //        OrderReference = result.OrderReference,
        //        Status = (TransactionStatus)result.Status
        //    }).ToList();
        //}
    }
}
