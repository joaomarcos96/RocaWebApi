using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RocaWebApi.Api.Features.Workers
{
    [ApiController]
    [Route("api/workers")]
    public class WorkersController : ControllerBase
    {
        private readonly IWorkerService _service;

        public WorkersController(IWorkerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerDto>>> GetWorkers()
        {
            var workers = await _service.GetAll();
            return Ok(workers?.Select(w => w.ToDto()));
        }

        [HttpGet("{workerId}", Name = "GetWorker")]
        public async Task<ActionResult<WorkerDto>> GetWorker(int workerId)
        {
            var worker = await _service.GetById(workerId);
            return worker == null
                ? (ActionResult<WorkerDto>) NotFound()
                : Ok(worker.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<WorkerDto>> CreateWorker(WorkerCreateDto workerDto)
        {
            var workerEntity = workerDto.ToEntity();

            await _service.Create(workerEntity);

            var createdWorkerDto = workerEntity.ToDto();

            return CreatedAtRoute(
                "GetWorker",
                new {workerId = createdWorkerDto.Id},
                createdWorkerDto);
        }

        [HttpDelete("{workerId}")]
        public async Task<ActionResult> DeleteWorker(int workerId)
        {
            await _service.Delete(workerId);

            return NoContent();
        }

        [HttpPut("{workerId}")]
        public async Task<ActionResult> UpdateWorker(
            int workerId,
            [FromBody] WorkerUpdateDto workerDto)
        {
            var workerEntity = workerDto.ToEntity();
            var workerUpdated = await _service.Update(workerId, workerEntity);

            return workerUpdated == null ? (ActionResult) NotFound() : NoContent();
        }
    }
}
