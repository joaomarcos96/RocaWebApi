using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using RocaWebApi.Api.Features.Auth;
using Xunit;

namespace RocaWebApi.Tests.Features.Auth
{
    [Collection(nameof(SharedFixture))]
    public class AuthControllerTest
    {
        private const string ResourceUrl = "/api/auth";

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public AuthControllerTest(SharedFixture fixture)
        {
            _client = fixture.Client;
            _jsonOptions = fixture.DefaultJsonSerializerOptions;
        }

        [Fact]
        public async Task Should_not_authenticate_with_incorrect_credentials()
        {
            var json = JsonSerializer.Serialize(new AuthRequest
            {
                Username = "Testando"
            }, _jsonOptions);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(ResourceUrl, stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
