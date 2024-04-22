using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace FlightControl.Test
{
    internal class MockConfig : IConfiguration
    {
        private readonly Dictionary<string, string> _configValues;

        public MockConfig()
        {
            _configValues = new()
            {
                { "Jwt:Key", "thisisatestkeythatmustbelongbecause256bitsmustbehere" },
                { "Jwt:Issuer", "test_issuer" }
            };
        }

        //None of the below methods are being used by the TokenService
        public string? this[string key]
        {
            get => _configValues.ContainsKey(key) ? _configValues[key] : null;
            set => _configValues[key] = value;
        }

        public IEnumerable<IConfigurationSection> GetChildren() => null;

        public IChangeToken GetReloadToken() => null;

        public IConfigurationSection GetSection(string key) => null;
    }
}
