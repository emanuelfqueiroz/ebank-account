using System.Net;
using System.Threading.Tasks;
using Application.Account;
using Application.Account.AccountQuery;
using Application.Transfer.Commands;
using CQRSHelper._Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(typeof(AccountResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AccountResponse>> Get([FromQuery] AccountQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("Transfer")]
        [ProducesResponseType(typeof(Response<decimal>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AccountResponse>> AddBalance([FromBody] TransferCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Status)
                return Ok(response);
            else
                return BadRequest(response);
        }

    }
}
