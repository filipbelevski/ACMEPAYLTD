using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payment.API.Application.Commands.Authorize;
using Payment.API.Application.Commands.Capture;
using Payment.API.Application.Commands.Void;
using Payment.API.Common.Contracts.Contracts;
using System;
using System.Threading.Tasks;

namespace Payment.API.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthorizeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Authorizes a payment
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AuthorizeResponse>> Authorize([FromBody]AuthorizeCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpPost("{id:guid}/voids")]
        public async Task<ActionResult<VoidResponse>> VoidTransaction(
            [FromRoute] Guid id,
            [FromBody] OrderReferenceDto orderReference)
        {
            VoidCommand command = new VoidCommand
            {
                Id = id,
                OrderReference = orderReference.OrderReference
            };

            return await mediator.Send(command);
        }

        [HttpPost("{id:guid}/capture")]
        public async Task<ActionResult<CaptureResponse>> CaptureTransaction(
            [FromRoute] Guid id,
            [FromBody] OrderReferenceDto orderReference)
        {
            CaptureCommand command = new CaptureCommand(id, orderReference.OrderReference);

            return await mediator.Send(command);
        }


    }
}
