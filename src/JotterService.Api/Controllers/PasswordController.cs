using JotterService.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotterService.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class PasswordController : ControllerBase
{
    private readonly ISender _sender;
    public PasswordController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _sender.Send(new GetAllPasswords.Request());
        return Ok(response);
    }

    // POST password/
    [HttpPost]
    public async Task<IActionResult> CreatePassword([FromBody] CreatePassword.Request request)
    {
        var response = await _sender.Send(request);
        return Ok(response);
    }

    // GET password/guid
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _sender.Send( new GetPassword.Request() { PasswordId = id});
        return Ok(response);
    }

    // PUT password/guid
    [HttpPut("{id}")]
    public Task<IActionResult> UpdatePassword(Guid id, [FromBody] CreatePassword.Request request)
    {
        throw new NotImplementedException($"{id} + {request}");

    }

    // DELETE password/guid
    [HttpDelete("{id}")]
    public Task DeletePassword(Guid id)
    {
        throw new NotImplementedException();
    }
}

