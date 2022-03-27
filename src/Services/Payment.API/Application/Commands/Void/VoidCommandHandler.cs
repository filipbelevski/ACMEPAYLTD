using MediatR;
using Payment.Domain.Exceptions;
using Payment.Domain.Model.Transaction;
using Payment.Domain.Model.Transaction.Enum;
using Payment.Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.API.Application.Commands.Void
{
    public class VoidCommandHandler : IRequestHandler<VoidCommand, VoidResponse>
    {
        private readonly ITransactionRepository transactionRepository;

        public VoidCommandHandler(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
        public async Task<VoidResponse> Handle(VoidCommand request, CancellationToken cancellationToken)
        {
            Transaction transaction = await transactionRepository.GetTransaction(request.Id);

            if(transaction == null)
            {
                throw new TransactionNotFoundException($"Transaction with guid: { request.Id } was not found.");
            }

            transaction.Status = TransactionStatus.Voided;

            Guid id = await transactionRepository.Update(transaction);


            return new VoidResponse
            {
                Id = id,
                Status = TransactionStatus.Voided
            };
        }
    }
}
