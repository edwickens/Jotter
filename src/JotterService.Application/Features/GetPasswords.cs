using MediatR;

namespace JotterService.Application.Features;

public class GetPasswords
{
    public class Request : IRequest<IEnumerable<Response>>
    {
        public Guid UserId { get; set; }
    }
    public record Response(Guid Id, Guid UserId, string Title, string Url,
    string Username, string Description, string CustomerNumber)
    {
        public string Secret => "**********";
    }

    public class Handler : IRequestHandler<Request,IEnumerable<Response>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index =>
            new Response
            (
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Password" + index.ToString(),
                $"https://{"Password" + index.ToString()}.com",
                "Username" + index.ToString(),
                "",
                ""
            ))
                ); 
        }
    }
}
