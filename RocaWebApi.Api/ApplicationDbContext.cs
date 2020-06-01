using System;
using Microsoft.EntityFrameworkCore;
using RocaWebApi.Api.Features.Workers;

namespace RocaWebApi.Api
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
