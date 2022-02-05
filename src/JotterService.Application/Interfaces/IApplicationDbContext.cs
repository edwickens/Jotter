using JotterService.Domain;
using Microsoft.EntityFrameworkCore;

namespace JotterService.Application
{
    public interface IApplicationDbContext
    {
        public DbSet<Password> Passwords { get; set; }
    }
}