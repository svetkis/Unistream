using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Unistream.TestTask.BLL.Transactions.Commands.AddTransaction;
using Unistream.TestTask.BLL.Transactions.Queries.GetTransaction;
using Unistream.TestTask.Common;
using Unistream.TestTask.Common.Dto;

namespace Unistream.TestTask.Server.Controllers;

[ApiController]
[Route("")]
[Produces("application/json")]
public class TransactionController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;
    private readonly IMediator _mediator;

    public TransactionController(ILogger<TransactionController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetTrasaction([FromQuery(Name = "get")] string id)
    {
        try
        {
            var dto = await _mediator.Send(new GetTransactionCommand { Id = id });
            return Ok(dto);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetTransactionDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    [FixQueryJsonStringFilter]
    public async Task<IActionResult> InsertTransaction(
        [FromQuery(Name = "insert")]
        [ModelBinder(BinderType = typeof(InsertTransactionModelBinder))] InsertTransactionModel data)
    {
        try
        {
            var result = await _mediator.Send(new InsertTransactionCommand { Transaction = data });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
