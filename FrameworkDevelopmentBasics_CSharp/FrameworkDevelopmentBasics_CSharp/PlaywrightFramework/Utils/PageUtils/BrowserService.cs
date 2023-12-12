using Autofac;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Autofac;
using Microsoft.Playwright;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.PageUtils
{
    public class BrowserService
    {
        public virtual IPage Driver => ContainerBase.Container.Resolve<IPage>();

        public BrowserService()
        {

        }

        public IPage Page()
        {
            return ContainerBase.Container.Resolve<IPage>();
        }

        public async Task RefreshAsync()
        {
            await Driver.ReloadAsync();
        }

        public async Task WaitForPageLoadAsync()
        {
            await Driver.WaitForLoadStateAsync(LoadState.Load);
        }

        public async Task CloseCurrentWindow()
        {
            await Driver.CloseAsync();
        }

        public string GetURL()
        {
            return Driver.Url;
        }

        public async Task QuitDriverAsync()
        {
            _ = DeleteAllCookiesAsync();
            await Driver.CloseAsync();
        }

        public async Task DeleteAllCookiesAsync()
        {
            await Driver.Context.ClearCookiesAsync();
        }
    }
}