using AutoMapper;
using RocaWebApi.Api.Features.Users;

namespace RocaWebApi.Api.Features.Workers
{
    public static class WorkersMapper
    {
        public static WorkerDto ToDto(this Worker worker)
        {
            var user = worker.User;
            if (user == null)
            {
                return null;
            }

            return new WorkerDto
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Phone,
                Address = user.Address
            };
        }

        public static Worker ToEntity(this WorkerManipulateDto workerDto)
        {
            return new Worker
            {
                User = new User
                {
                    Name = workerDto.Name,
                    Phone = workerDto.Phone,
                    Address = workerDto.Address
                }
            };
        }

        public static Worker ToEntity(this WorkerCreateDto workerDto)
        {
            return new Worker
            {
                User = new User
                {
                    Name = workerDto.Name,
                    Phone = workerDto.Phone,
                    Address = workerDto.Address
                }
            };
        }

        public static Worker ToEntity(this WorkerUpdateDto workerDto)
        {
            return new Worker
            {
                User = new User
                {
                    Name = workerDto.Name,
                    Phone = workerDto.Phone,
                    Address = workerDto.Address
                }
            };
        }

        public static void From(this Worker worker, Worker fromWorker)
        {
            worker.User.Name = fromWorker.User.Name ?? worker.User.Name;
            worker.User.Address = fromWorker.User.Address ?? worker.User.Address;
            worker.User.Phone = fromWorker.User.Phone ?? worker.User.Phone;
        }
    }
}
