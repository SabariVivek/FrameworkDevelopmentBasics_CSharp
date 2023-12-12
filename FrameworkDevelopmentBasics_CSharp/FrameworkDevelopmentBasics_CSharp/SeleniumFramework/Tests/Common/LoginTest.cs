using NUnit.Framework;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Tests.Common
{
    public class LoginTest : TestSuiteFixture
    {
        [Test]
        public void LoginLogoutTest()
        {
            var LoginPage = PageObjectReg.CommonPages.LoginPage;

            Step("Login into an application");
            LoginPage.Login(Data.Admin);

            Step("Logout from the application");
            LoginPage.Logout();
        }
    }
}