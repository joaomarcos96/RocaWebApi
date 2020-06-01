using System;
using AutoMapper;

namespace RocaWebApi.Api.Features.Workers
{
    public class WorkersMapper : Profile
    {
        public WorkersMapper()
        {
            CreateMap<Worker, WorkerDto>();
            CreateMap<WorkerCreateDto, Worker>();
        }
    }
}
