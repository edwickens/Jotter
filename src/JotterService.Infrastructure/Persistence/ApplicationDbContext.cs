﻿using JotterService.Application;
using JotterService.Domain;
using Microsoft.EntityFrameworkCore;

namespace JotterService.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Password> Passwords { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options){}
    }
}