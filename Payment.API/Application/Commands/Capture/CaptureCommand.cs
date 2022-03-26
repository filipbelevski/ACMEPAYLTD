using MediatR;
using System;

namespace Payment.API.Application.Commands.Capture
{
    public class CaptureCommand : IRequest<CaptureResponse>
    {
        public CaptureCommand(Guid id, string orderReference)
        {
            Id = id;
            OrderReference = orderReference;
        }
        public Guid Id { get; set; }

        public string OrderReference { get; set; }
    }
}
