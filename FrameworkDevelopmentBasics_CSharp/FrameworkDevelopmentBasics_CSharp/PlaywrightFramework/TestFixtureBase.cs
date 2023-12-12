using Autofac;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Autofac;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.PageUtils;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.TestConfigurations;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Resources;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework
{
    [TestFixture]
    public class TestFixtureBase : ContainerBase
    {
        protected readonly BrowserService browserService = new BrowserService();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(GetProjectDirectory() + "\\PlaywrightFramework\\Resources\\config.json")
                .Build();

            var testConfig = configuration.GetSection("site").Get<TestConfiguration>()!;
            SetupAutofac(testConfig);

            // Initializing Test Data Service...
            var testDataService = Container.Resolve<TestDataService>();
            testDataService.LoadData();
        }

        private static void SetupAutofac(TestConfiguration testConfig)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(testConfig).As<ITestConfiguration>();
            builder.RegisterModule(new CoreWebTestModule(testConfig));
            builder.RegisterModule(new PageObjectWebTestModule(testConfig));

            Container = builder.Build();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            try
            {
                if (Container.Resolve<IWebDriver>() != null)
                    Container.Resolve<IWebDriver>().Quit();
            }
            catch { }
        }

        public static string GetProjectDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            int binIndex = baseDirectory.IndexOf("\\bin\\", StringComparison.OrdinalIgnoreCase);
            return baseDirectory.Substring(0, binIndex);
        }
    }
}