using System;
using Microsoft.EntityFrameworkCore;

namespace RocaWebApi.Api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
