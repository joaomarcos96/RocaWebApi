using System.ComponentModel.DataAnnotations;

namespace RocaWebApi.Api.Features.Auth
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
