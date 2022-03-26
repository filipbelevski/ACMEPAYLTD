using MediatR;
using System;

namespace Payment.API.Application.Commands.Void
{
    public class VoidCommand : IRequest<VoidResponse>
    {
        public VoidCommand()
        {

        }
        public VoidCommand(Guid id, string orderReference)
        {
            Id = id;
            OrderReference = orderReference;
        }
        public Guid Id { get; set; }

        public string OrderReference { get; set; }
    }
}
