using System.Collections.Generic;
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
            return Ok(_mapper.Map<IEnumerable<WorkerDto>>(workers));
        }

        [HttpGet("{workerId}", Name = "GetWorker")]
        public async Task<ActionResult<WorkerDto>> GetWorker(int workerId)
        {
            var worker = await _service.GetById(workerId);
            return worker == null
                ? (ActionResult<WorkerDto>) NotFound()
                : Ok(_mapper.Map<WorkerDto>(worker));
        }

        [HttpPost]
        public async Task<ActionResult<WorkerDto>> CreateWorker(WorkerCreateDto workerDto)
        {
            var user = _mapper.Map<Worker>(workerDto);

            await _service.Create(user);

            var worker = _mapper.Map<WorkerDto>(user);

            return CreatedAtRoute(
                "GetWorker",
                new {workerId = worker.Id},
                worker);
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
            var workerEntity = _mapper.Map<Worker>(workerDto);
            var workerUpdated = await _service.Update(workerId, workerEntity);

            return workerUpdated == null ? (ActionResult) NotFound() : NoContent();
        }
    }
}
