using AutoMapper;
using MediatR;
using Payment.Domain.Model.Transaction;
using Payment.Domain.Model.Transaction.Enum;
using Payment.Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.API.Application.Commands.Authorize
{
    public class AuthorizeCommandHandler : IRequestHandler<AuthorizeCommand, AuthorizeResponse>
    {
        private readonly ITransactionRepository commandRepository;
        private readonly IMapper mapper;

        public AuthorizeCommandHandler(ITransactionRepository commandRepository, IMapper mapper)
        {
            this.commandRepository = commandRepository;
            this.mapper = mapper;
        }

        public async Task<AuthorizeResponse> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
        {
            Transaction transaction = mapper.Map<Transaction>(request);

            Guid paymentUid = await commandRepository.AuthorizeTransaction(transaction);

            AuthorizeResponse response = new AuthorizeResponse
            {
                Id = paymentUid,
                Status = TransactionStatus.Authorized
            };

            return response;

        }
    }
}
