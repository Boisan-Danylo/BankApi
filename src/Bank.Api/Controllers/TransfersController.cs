using Bank.Api.Mappers;
using Bank.Application.Interfaces;
using Bank.Contracts;
using Bank.Domain.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class TransfersController : ControllerBase
    {
        private readonly IAccountTransactionsService _tx;
        public TransfersController(IAccountTransactionsService tx) => _tx = tx;

        [HttpPost]
        [ProducesResponseType(typeof(TransferResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] TransferRequest request)
        {
            var (from, to) = _tx.Transfer(request.FromAccount, request.ToAccount, request.Amount);
            return Ok(new TransferResultDto(from.ToDto(), to.ToDto()));
        }

        [HttpPost("{accountNumber}/deposit")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult Deposit(string accountNumber, [FromBody] MoneyRequest request)
        {
            var acc = _tx.Deposit(accountNumber, request.Amount);
            return Ok(acc.ToDto());
        }

        [HttpPost("{accountNumber}/withdraw")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult Withdraw(string accountNumber, [FromBody] MoneyRequest request)
        {
            var acc = _tx.Withdraw(accountNumber, request.Amount);
            return Ok(acc.ToDto());
        }
    }
}
