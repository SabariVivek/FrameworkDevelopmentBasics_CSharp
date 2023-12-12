using Autofac;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Autofac;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Reports;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Resources;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.PageUtils;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.TestConfigurations;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework
{
    [TestFixture]
    public class TestFixtureBase : ContainerBase
    {
        protected readonly BrowserService browserService = new BrowserService();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(GetProjectDirectory() + "\\SeleniumFramework\\Resources\\config.json")
                .Build();

            var testConfig = configuration.GetSection("site").Get<TestConfiguration>()!;
            SetupAutofac(testConfig);
            ExtentTestManager.CreateParentTest(GetType().Name);

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
                ExtentManager.Instance.Flush();
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