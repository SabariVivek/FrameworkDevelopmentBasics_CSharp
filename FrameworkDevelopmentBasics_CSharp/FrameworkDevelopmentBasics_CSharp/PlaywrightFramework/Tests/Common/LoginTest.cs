using NUnit.Framework;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Tests.Common
{
    public class LoginTest : TestSuiteFixture
    {
        [Test]
        public async Task LoginLogoutTestAsync()
        {
            var LoginPage = PageObjectReg.CommonPages.LoginPage;

            await LoginPage.LoginAsync(Data.Admin);
            await LoginPage.LogoutAsync();
        }
    }
}