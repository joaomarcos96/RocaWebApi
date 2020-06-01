using System;
using System.ComponentModel.DataAnnotations;

namespace RocaWebApi.Api.Features.Workers
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }
    }
}
