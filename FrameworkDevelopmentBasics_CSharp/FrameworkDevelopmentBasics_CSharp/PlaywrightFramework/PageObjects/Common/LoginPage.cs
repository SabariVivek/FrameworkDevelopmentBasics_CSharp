using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.PageObjects.Base;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Resources.User;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.PageUtils;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.PageObjects.Common
{
    public class LoginPage : BasePage, IPageSection
    {
        public readonly LoginPageLocators obj;
        public BrowserService browserService;

        public LoginPage()
        {
            obj = new LoginPageLocators();
            browserService = new BrowserService();
        }

        public async Task LoginAsync(Credentials cred)
        {
            await GoToPageAsync(Configuration.BaseQAUrl);
            await obj.Username.TypeTextAsync(cred.UserName);
            await obj.Password.TypeTextAsync(cred.Password);
            await obj.Login.ClickAsync();
            await obj.Logout.ElementDisplayed();
        }

        public async Task LogoutAsync()
        {
            await obj.Logout.ClickAsync();
        }
    }
}