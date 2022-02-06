using JotterService.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JotterService.Api;

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
    public async Task<IEnumerable<GetPasswords.Response>> Get()
    {
        return await _sender.Send(new GetPasswords.Request());
    }

    // GET password/guid
    //[HttpGet("{id}")]
    //public Password Get(Guid id)
    //{
    //    return new Password
    //        (
    //            id,
    //            Guid.NewGuid(),
    //            "Password" + id.ToString(),
    //            $"https://{"Password" + id.ToString()}.com",
    //            "Username" + id.ToString(),
    //            "",
    //            ""
    //        );
    //}

    // POST password/
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT password/guid
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE password/guid
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

