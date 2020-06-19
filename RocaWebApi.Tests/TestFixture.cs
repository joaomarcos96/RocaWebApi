using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RocaWebApi.Api;
using RocaWebApi.Api.Features.Users;
using RocaWebApi.Api.Features.Workers;

namespace RocaWebApi.Tests
{
    public class TestFixture
    {
        private DbConnection _connection;

        private readonly TestServer _server;

        public HttpClient Client { get; }

        public JsonSerializerOptions DefaultJsonSerializerOptions { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Testing")
                .ConfigureServices(services =>
                {
                    _connection = new SqliteConnection("DataSource=file::memory:?cache=shared");
                    _connection.Open();

                    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(_connection));

                    var serviceProvider = services.BuildServiceProvider();

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();
                        InitializeDatabase(dbContext);
                    }
                });

            _server = new TestServer(builder);

            Client = _server.CreateClient();

            DefaultJsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        private void InitializeDatabase(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            SeedDatabase(dbContext);
        }

        private void SeedDatabase(ApplicationDbContext dbContext)
        {
            dbContext.Workers.AddRange(
                new List<Worker>
                {
                    new Worker
                    {
                        Id = 1,
                        User = new User
                        {
                            Id = 1,
                            Name = "Lucas Costa",
                            Phone = "(35) 12345-6789",
                            Address = "Distrito de Costas - MG"
                        }
                    },
                    new Worker
                    {
                        Id = 2,
                        User = new User
                        {
                            Id = 2,
                            Name = "Worker to be deleted"
                        }
                    }
                }
            );

            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();

            _connection?.Close();
            _connection = null;
        }
    }
}
