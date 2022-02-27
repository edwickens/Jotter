using JotterService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JotterService.Application.Features;

public class GetPassword
{
    public class Request : IRequest<Response>
    {
        public Guid UserId { get; set; }
        public Guid PasswordId { get; set; }
    }

    public record Response(string Secret) { }

    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEncryptionService _encryptionService;

        public Handler(IApplicationDbContext context, IEncryptionService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var password =  await _context.Passwords
                .SingleOrDefaultAsync(p => p.Id.Equals(request.PasswordId), cancellationToken);

            if (password == null) // TODO: error controller 
                throw new KeyNotFoundException();

            var secret = _encryptionService.Decrypt(password.Secret);
            return new Response(secret);
        }
    }
}
