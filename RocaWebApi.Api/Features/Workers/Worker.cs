using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RocaWebApi.Api.Base.Entity;
using RocaWebApi.Api.Features.Users;

namespace RocaWebApi.Api.Features.Workers
{
    public class Worker : TrackableEntity
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public User User { get; set; } = new User();
    }
}
