using RocaWebApi.Api.Features.Users;

namespace RocaWebApi.Api.Features.Auth
{
    public class AuthResponse
    {
        public string Name { get; }
        public string Phone { get; }
        public string Address { get; }
        public string Token { get; }

        public AuthResponse(User user, string token)
        {
            Name = user.Name;
            Phone = user.Phone;
            Address = user.Address;
            Token = token;
        }
    }
}
