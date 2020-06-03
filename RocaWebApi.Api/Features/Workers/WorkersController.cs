using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace RocaWebApi.Api.Features.Workers
{
    [ApiController]
    [Route("api/workers")]
    public class WorkersController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public WorkersController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerDto>>> GetWorkers()
        {
            var workers = await _dbContext.Workers.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<WorkerDto>>(workers));
        }

        [HttpGet("{workerId}", Name = "GetWorker")]
        public async Task<ActionResult<WorkerDto>> GetWorker(int workerId)
        {
            var worker = await _dbContext.Workers.FirstOrDefaultAsync(worker => worker.Id == workerId);
            if (worker == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WorkerDto>(worker));
        }

        [HttpPost]
        public async Task<ActionResult<WorkerDto>> CreateWorker(WorkerCreateDto workerDto)
        {
            var workerEntity = _mapper.Map<Worker>(workerDto);

            _dbContext.Workers.Add(workerEntity);
            await _dbContext.SaveChangesAsync();

            var worker = _mapper.Map<WorkerDto>(workerEntity);

            return CreatedAtRoute("GetWorker", new { workerId = worker.Id }, worker);
        }

        [HttpDelete("{workerId}")]
        public async Task<ActionResult> DeleteWorker(int workerId)
        {
            var worker = await _dbContext.Workers.FirstOrDefaultAsync(worker => worker.Id == workerId);
            if (worker == null)
            {
                return NoContent();
            }

            _dbContext.Workers.Remove(worker);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{workerId}")]
        public async Task<ActionResult> UpdateWorker(int workerId, [FromBody] WorkerUpdateDto workerDto)
        {
            var workerEntity = await _dbContext.Workers.FirstOrDefaultAsync(worker => worker.Id == workerId);
            if (workerEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(workerDto, workerEntity);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
