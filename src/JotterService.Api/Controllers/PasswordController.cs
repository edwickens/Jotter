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
    public async Task<IEnumerable<GetAllPasswords.Response>> Get()
    {
        return await _sender.Send(new GetAllPasswords.Request());
    }

    // GET password/guid
    [HttpGet("{id}")]
    public Task Get(Guid id)
    {
        throw new NotImplementedException();
    }

    // POST password/
    [HttpPost]
    public async Task<CreatePassword.Response> CreatePassword([FromBody] CreatePassword.Request request)
    {
        return await _sender.Send(request);
    }

    // PUT password/guid
    [HttpPut("{id}")]
    public Task<CreatePassword.Response> UpdatePassword(Guid id, [FromBody] CreatePassword.Request request)
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

