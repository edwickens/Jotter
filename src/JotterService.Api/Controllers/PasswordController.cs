using Microsoft.AspNetCore.Mvc;

namespace JotterService.Api;

[Route("[controller]")]
[ApiController]
public class PasswordController : ControllerBase
{
    [HttpGet]
    public IEnumerable<Password> Get()
    {
        return Enumerable.Range(1, 5).Select(index =>
            new Password
            (
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Password" + index.ToString(),
                $"https://{"Password" + index.ToString()}.com",
                "Username" + index.ToString(),
                "",
                ""
            ))
            .ToArray();
    }

    // GET password/guid
    [HttpGet("{id}")]
    public Password Get(Guid id)
    {
        return new Password
            (
                id,
                Guid.NewGuid(),
                "Password" + id.ToString(),
                $"https://{"Password" + id.ToString()}.com",
                "Username" + id.ToString(),
                "",
                ""
            );
    }

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

public record Password(Guid Id, Guid UserId, string Title, string Url,
string Username, string Description, string CustomerNumber)
{
    public string Secret => "**********";
}

