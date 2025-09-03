using Bank.Api.Mappers;
using Bank.Application.Interfaces;
using Bank.Contracts;
using Bank.Domain.Accounts;
using Bank.Domain.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AccountsController : ControllerBase
    {
        private readonly IAccountService _crud;
        private readonly IAccountTransactionsService _tx;

        public AccountsController(IAccountService crud, IAccountTransactionsService tx)
        { _crud = crud; _tx = tx; }

        [HttpPost]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] CreateAccountRequest request)
        {
            var acc = _crud.Create(request.OwnerName, request.InitialBalance, request.Currency ?? "USD");
            var dto = acc.ToDto();
            return CreatedAtAction(nameof(GetById), new { accountNumber = acc.AccountNumber }, dto);
        }

        [HttpGet("{accountNumber}")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(string accountNumber)
        {
            var acc = _crud.Get(accountNumber);
            return Ok(acc.ToDto());
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
        public IActionResult List()
        {
            var list = _crud.List().Select(x => x.ToDto());
            return Ok(list);
        }
    }
}
