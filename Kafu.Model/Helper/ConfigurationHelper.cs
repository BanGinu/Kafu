using Microsoft.Extensions.Configuration;

namespace Kafu.Model.Helper
{
    public static class ConfigurationHelper
    {
        private static IConfiguration Configuration;

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration GetConfigurations() => Configuration;

        public static IConfigurationSection GetSection(string section) => Configuration.GetSection(section);

        // Value must not be an object or array otherwise use GetSection function
        public static T GetValue<T>(string path) => Configuration.GetValue<T>(path);

    }
}
