namespace RocaWebApi.Api.Config
{
    public class JwtSettingsOptions
    {
        public const string JwtSettings = "JwtSettings";

        public int ExpiresInDays { get; set; }
        public string Secret { get; set; }
    }
}
