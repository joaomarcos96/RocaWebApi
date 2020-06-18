using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RocaWebApi.Api.Features.Workers
{
    public interface IWorkerService
    {
        Task<IEnumerable<Worker>> GetAll();
        Task<Worker> GetById(int id);
        Task<Worker> Create(Worker worker);
        Task<Worker> Update(int workerId, Worker worker);
        Task Delete(int id);
    }

    public class WorkerService : IWorkerService
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Worker>> GetAll()
        {
            var workers = await _dbContext.Workers.ToListAsync();
            return workers;
        }

        public Task<Worker> GetById(int id)
        {
            return _dbContext
                .Workers
                .Include(worker => worker.User)
                .FirstOrDefaultAsync(w => w.User.Id == id);
        }

        public async Task<Worker> Create(Worker worker)
        {
            await _dbContext.Workers.AddAsync(worker);
            await _dbContext.SaveChangesAsync();

            return worker;
        }

        public async Task<Worker> Update(int id, Worker worker)
        {
            var workerEntity = await _dbContext.Workers.FirstOrDefaultAsync(w => w.User.Id == id);
            if (workerEntity == null)
            {
                return null;
            }

            workerEntity.From(worker);

            await _dbContext.SaveChangesAsync();

            return workerEntity;
        }

        public async Task Delete(int id)
        {
            var worker = await _dbContext.Workers.FirstOrDefaultAsync(w => w.User.Id == id);
            if (worker == null)
            {
                return;
            }

            _dbContext.Workers.Remove(worker);
            await _dbContext.SaveChangesAsync();
        }
    }
}
