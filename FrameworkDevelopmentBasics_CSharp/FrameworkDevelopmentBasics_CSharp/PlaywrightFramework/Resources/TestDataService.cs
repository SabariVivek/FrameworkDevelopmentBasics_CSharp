using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Resources.User;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.TestConfigurations;
using Newtonsoft.Json;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Resources
{
    public class TestDataService
    {
        private readonly ITestConfiguration _config;
        public UserType? Data { get; set; }

        public TestDataService(ITestConfiguration config)
        {
            _config = config;
        }

        public static UserType GetData(ITestConfiguration config)
        {
            var dataFile = Path.Combine(TestFixtureBase.GetProjectDirectory(), "PlaywrightFramework", "Resources", "credentials.json");
            var data = JsonConvert.DeserializeObject<UserType>(File.ReadAllText(dataFile));
            return data!;
        }

        public void LoadData()
        {
            Data = GetData(_config);
        }
    }
}