using System.ComponentModel.DataAnnotations.Schema;
using RocaWebApi.Api.Base.Entity;
using RocaWebApi.Api.Features.Users;

namespace RocaWebApi.Api.Features.Workers
{
    public class Worker : TrackableEntity
    {
        [ForeignKey("Id")]
        public User User { get; set; } = new User();
    }
}
