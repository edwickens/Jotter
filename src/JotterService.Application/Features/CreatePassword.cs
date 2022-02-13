using JotterService.Application.Interfaces;
using JotterService.Domain;
using MediatR;

namespace JotterService.Application.Features;

public class CreatePassword
{
    public class Request : IRequest<Response>
    {
        public Request(string secret)
        {
            Secret = secret;
        }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? Username { get; set; }
        public string? Description { get; set; }
        public string? CustomerNumber { get; set; }
        public string Secret { get; set; }
    }
    public record Response(Guid Id, Guid UserId, string? Title, string? Url,
    string? Username, string? Description, string? CustomerNumber)
    {
        public static string Secret => "**********";
    }

    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            // TODO: use automapper? 
            var password = new Password {
                UserId = request.UserId,
                Title = request.Title,
                Url = request.Url,
                Username = request.Username,
                Description = request.Description,
                CustomerNumber = request.CustomerNumber,
                Secret = request.Secret
            };
            await _context.Passwords.AddAsync(password, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new Response(
                password.Id,
                password.UserId,
                password.Title,
                password.Url,
                password.Username,
                password.Description,
                password.CustomerNumber);
        }
    }
}
