using Autofac;
using Module = Autofac.Module;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using System.Reflection;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.TestConfigurations;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements.Base;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Autofac
{
    public class CoreWebTestModule : Module
    {
        private readonly ITestConfiguration _configuration;
        readonly string? RootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public CoreWebTestModule(ITestConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Chrome Options Configuration...
            var chromeOptions = new ChromeOptions
            {
                AcceptInsecureCertificates = _configuration.AcceptInsecureCertificates
            };
            chromeOptions.AddArgument("--start-maximized");

            // Headless Configuration...
            bool IsHeadless = false;
            if (IsHeadless)
                chromeOptions.AddArgument("--headless");

            // Browser Configuration...
            string browser = Environment.GetEnvironmentVariable("Browser")!;
            if (browser != null)
            {
                _configuration.Browser = browser;
            }

            // Driver - Autofac Configuration...
            if (_configuration.Browser == "Chrome")
            {
                builder.RegisterType<ChromeDriver>()
                    .WithParameter(new TypedParameter(typeof(string), $"{RootDirectory}"))
                    .WithParameter(new TypedParameter(typeof(ChromeOptions), chromeOptions))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

            }
            else if (_configuration.Browser == "MsEdge")
            {
                var service = EdgeDriverService.CreateDefaultService(RootDirectory, "msedgedriver.exe");
                service.UseVerboseLogging = true;
                service.Start();

                builder.RegisterType<EdgeDriver>()
                    .WithParameter(new TypedParameter(typeof(EdgeDriverService), service))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }
            else
            {
                throw new Exception($"Check the browser data in Config.json, Used value - {_configuration.Browser}");
            }

            // Web Element - Autofac Configuration...
            var testSiteAssembly = Assembly.GetAssembly(typeof(ElementBase))!;
            builder.RegisterAssemblyTypes(testSiteAssembly).Where(t => typeof(ElementBase).IsAssignableFrom(t)).InstancePerDependency().AsSelf();
        }
    }
}
