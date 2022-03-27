using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payment.API.Application.Queries.Transactions.List;
using Payment.API.Common.Contracts.Responses;
using Payment.Domain.Model.Transaction.Enum;
using System;
using System.Threading.Tasks;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TransactionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ListResultDto<TransactionsListQueryResponse>>> GetTransactions(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] string searchString,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] TransactionStatus status
            )
        {
            TransactionsListQuery query = new TransactionsListQuery
            {
                PageSize = pageSize,
                CurrentPage = page,
                SearchString = searchString,
                From = from,
                To = to,
                Status = status
            };

            return await mediator.Send(query);
        }
    }
}
