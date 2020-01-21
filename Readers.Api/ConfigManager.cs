using Microsoft.Extensions.Configuration;

namespace ReadersApi.Providers
{
    public interface IConfigManager
    {
        string Issuer { get; }
        string Audience { get; }
        int ExpiryInMinutes { get; }
        string Key { get; }
        string ConnectionString { get; }
    }
    public class ConfigManager : IConfigManager
    {
        private readonly IConfiguration _config;

        public ConfigManager(IConfiguration config)
        {
            _config = config;
        }

        public string Issuer => _config["Auth:Issuer"];

        public string Audience => _config["Auth:Audience"];

        public int ExpiryInMinutes => int.Parse(_config["Auth:ExpiryInMinutes"]);

        public string Key => _config["Auth:Key"];

        public string ConnectionString => _config.GetConnectionString("Auth:DefaultConnection");
    }
}