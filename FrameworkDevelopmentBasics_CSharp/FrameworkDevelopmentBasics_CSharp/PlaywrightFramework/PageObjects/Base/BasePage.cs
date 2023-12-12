using Autofac;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Autofac;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.TestConfigurations;
using Microsoft.Playwright;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.PageObjects.Base
{
    public class BasePage
    {
        public IBrowserContext context => ContainerBase.Container.Resolve<IBrowserContext>();
        public IPage Driver => ContainerBase.Container.Resolve<IPage>();
        protected ITestConfiguration Configuration => ContainerBase.Container.Resolve<ITestConfiguration>();

        public async Task GoToPageAsync(string URL)
        {
            Console.WriteLine("<<<<< Browser Launching >>>>>");
            await Driver.GotoAsync(URL);
        }
    }
}