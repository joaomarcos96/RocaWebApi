using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RocaWebApi.Api.Features.Workers;
using Xunit;

namespace RocaWebApi.Tests.Features.Workers
{
    public class WorkersControllerTest : IClassFixture<TestFixture>
    {
        private const string RESOURCE_URL = "/api/workers";

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public WorkersControllerTest(TestFixture fixture)
        {
            _client = fixture.Client;
            _jsonOptions = fixture.DefaultJsonSerializerOptions;
        }

        [Fact]
        public async Task Should_return_all_workers()
        {
            var response = await _client.GetAsync(RESOURCE_URL);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_worker_that_does_not_exist_should_return_not_found()
        {
            var response = await _client.GetAsync($"{RESOURCE_URL}/0");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_newly_created_worker_should_return_that_worker()
        {
            var responseForPostValidWorker = await PostValidWorker();
            var jsonForPostWorker = await responseForPostValidWorker.Content.ReadAsStringAsync();
            var createdWorker = JsonSerializer.Deserialize<Worker>(jsonForPostWorker, _jsonOptions);

            var responseForGetWorker = await _client.GetAsync($"{RESOURCE_URL}/{createdWorker.Id}");
            var jsonForGetWorker = await responseForGetWorker.Content.ReadAsStringAsync();
            var returnedWorker = JsonSerializer.Deserialize<Worker>(jsonForGetWorker, _jsonOptions);

            Assert.Equal(returnedWorker.Id, createdWorker.Id);

            Assert.Equal(HttpStatusCode.OK, responseForGetWorker.StatusCode);
        }

        [Fact]
        public async Task Valid_input_should_create_worker()
        {
            var response = await PostValidWorker();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Missing_required_property_post_should_return_unprocessable_entity()
        {
            var json = JsonSerializer.Serialize(new Worker(), _jsonOptions);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(RESOURCE_URL, stringContent);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Fact]
        public async Task Null_json_body_should_return_bad_request()
        {
            var stringContent = new StringContent("null", Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(RESOURCE_URL, stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private Task<HttpResponseMessage> PostValidWorker()
        {
            var json = JsonSerializer.Serialize(new Worker
            {
                Name = "Lúcãs com âçénto",
                Phone = "(35) 12345-6789",
                Address = "Distrito de Costas - MG"
            }, _jsonOptions);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            return _client.PostAsync(RESOURCE_URL, stringContent);
        }
    }
}
