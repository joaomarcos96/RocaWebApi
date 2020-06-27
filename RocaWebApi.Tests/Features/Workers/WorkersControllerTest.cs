using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using RocaWebApi.Api.Features.Workers;
using Xunit;

namespace RocaWebApi.Tests.Features.Workers
{
    [Collection(nameof(SharedFixture))]
    public class WorkersControllerTest
    {
        private const string ResourceUrl = "/api/workers";

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public WorkersControllerTest(SharedFixture fixture)
        {
            _client = fixture.Client;
            _jsonOptions = fixture.DefaultJsonSerializerOptions;
        }

        [Fact]
        public async Task Should_return_all_workers()
        {
            var response = await _client.GetAsync(ResourceUrl);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_worker_that_does_not_exist_should_return_not_found()
        {
            var response = await _client.GetAsync($"{ResourceUrl}/0");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_worker_should_return_ok()
        {
            var response = await _client.GetAsync($"{ResourceUrl}/1");
            var json = await response.Content.ReadAsStringAsync();
            var returnedWorker = JsonSerializer.Deserialize<WorkerDto>(json, _jsonOptions);

            returnedWorker.Id.Should().Be(1);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Valid_input_should_create_worker()
        {
            var json = JsonSerializer.Serialize(new WorkerCreateDto
            {
                Name = "Lúcãs com âçénto",
                Phone = "(35) 12345-6789",
                Address = "Distrito de Costas - MG"
            }, _jsonOptions);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(ResourceUrl, stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Missing_required_property_post_should_return_unprocessable_entity()
        {
            var json = JsonSerializer.Serialize(new WorkerCreateDto(), _jsonOptions);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(ResourceUrl, stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }

        [Fact]
        public async Task Null_json_body_should_return_bad_request()
        {
            var stringContent = new StringContent("null", Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(ResourceUrl, stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_delete_worker()
        {
            var response = await _client.DeleteAsync($"{ResourceUrl}/2");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Invalid_input_should_return_bad_request()
        {
            const string json = "{\"name\": 123}";
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(ResourceUrl, stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_worker_should_return_no_content()
        {
            var json = JsonSerializer.Serialize(new WorkerUpdateDto
            {
                Name = "Different name",
                Phone = "(35) 12345-6789",
                Address = "Distrito de Costas - MG"
            }, _jsonOptions);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{ResourceUrl}/1", stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Update_worker_that_does_not_exist_should_return_not_found()
        {
            var json = JsonSerializer.Serialize(
                new WorkerUpdateDto
                {
                    Name = "Different name"
                }, _jsonOptions);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{ResourceUrl}/0", stringContent);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
