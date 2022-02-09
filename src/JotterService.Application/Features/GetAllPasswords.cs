using JotterService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JotterService.Application.Features;

public class GetAllPasswords
{
    public class Request : IRequest<IEnumerable<Response>>
    {
        public Guid UserId { get; set; }
    }
    public record Response(Guid Id, Guid UserId, string? Title, string? Url,
    string? Username, string? Description, string? CustomerNumber)
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

        public async Task<IEnumerable<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _context.Passwords
                .OrderBy(p => p.Title)
                .Select(p=>
                    new Response
                    (
                        p.Id,
                        p.UserId,
                        p.Title,
                        p.Url,
                        p.Username,
                        p.Description,
                        p.CustomerNumber
                    ))
                .ToListAsync(cancellationToken);
        }
    }
}
