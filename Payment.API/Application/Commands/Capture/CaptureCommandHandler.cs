using MediatR;
using Payment.Domain.Exceptions;
using Payment.Domain.Model.Transaction;
using Payment.Domain.Model.Transaction.Enum;
using Payment.Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.API.Application.Commands.Capture
{
    public class CaptureCommandHandler : IRequestHandler<CaptureCommand, CaptureResponse>
    {
        private readonly ITransactionRepository transactionRepository;

        public CaptureCommandHandler(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }
        public async Task<CaptureResponse> Handle(CaptureCommand request, CancellationToken cancellationToken)
        {
            Transaction transaction = await transactionRepository.GetTransaction(request.Id);

            if (transaction == null)
            {
                throw new TransactionNotFoundException($"Transaction with guid: { request.Id } was not found.");
            }

            transaction.Status = TransactionStatus.Captured;

            Guid id = await transactionRepository.Update(transaction);

            return new CaptureResponse
            {
                Id = id,
                Status = transaction.Status
            };
        }
    }
}
