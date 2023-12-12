using Autofac;
using Module = Autofac.Module;
using System.Reflection;
using Microsoft.Playwright;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.TestConfigurations;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.WebElements.Base;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Autofac
{
    public class CoreWebTestModule : Module
    {
        private readonly ITestConfiguration _configuration;

        public static IPlaywright playwright { get; set; }
        public static IPage Page { get; set; }
        public static IBrowserContext browserContext { get; set; }
        public static IBrowser browser { get; set; }

        public CoreWebTestModule(ITestConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            Task.Run(async () => await RegisterBrowser(builder)).Wait();

            var testSiteAssembly = Assembly.GetAssembly(typeof(ElementBase))!;
            builder.RegisterAssemblyTypes(testSiteAssembly).Where(t => typeof(ElementBase)
                .IsAssignableFrom(t))
                .InstancePerDependency()
                .AsSelf();
        }

        private async Task RegisterBrowser(ContainerBuilder builder)
        {
            // Playwrigt Declaration...
            playwright = await Playwright.CreateAsync();
            bool _headless = _configuration.Headless;

            // Browser Type Selection...
            if (_configuration.Browser.Equals("firefox"))
            {
                browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Channel = _configuration.Browser,
                    Headless = _headless,
                }).ConfigureAwait(false);
            }
            else if (_configuration.Browser.Equals("safari"))
            {
                browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Channel = _configuration.Browser,
                    Headless = _headless,
                }).ConfigureAwait(false);
            }
            else if (_configuration.Browser.Equals("chrome"))
            {
                browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Channel = _configuration.Browser,
                    Headless = _headless,
                }).ConfigureAwait(false);
            }
            else
            {
                browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Channel = "chrome",
                    Headless = false,
                    Args = new[] { "--start-maximized" },
                }).ConfigureAwait(false);
            }

            // For Maximum View Mode...
            var contextOptions = new BrowserNewContextOptions()
            {
                ViewportSize = ViewportSize.NoViewport
            };

            // Browser and Page Configuration...
            browserContext = await browser.NewContextAsync(contextOptions);
            Page = await browserContext.NewPageAsync();

            // Autofac Configuration...
            builder.RegisterInstance(Page).AsImplementedInterfaces();
            builder.RegisterInstance(browserContext).AsImplementedInterfaces();
            builder.RegisterInstance(playwright).AsImplementedInterfaces();
        }
    }
}
