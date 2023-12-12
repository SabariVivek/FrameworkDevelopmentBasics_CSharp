using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Base;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Resources.User;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.PageUtils;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Common
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

        public void Login(Credentials cred)
        {
            GoToPage();
            obj.Username.TypeText(cred.UserName);
            obj.Password.TypeText(cred.Password);
            obj.Login.Click();
            obj.Logout.ElementDisplayed();
        }

        public void Logout()
        {
            obj.Logout.Click();
        }
    }
}