using OpenQA.Selenium;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebdriverWait
{
    public interface IWait
    {
        bool Until(Func<IWebDriver, bool> condition);

        IWebDriver Until(Func<IWebDriver, IWebDriver> condition);

        IWebElement Until(Func<IWebDriver, IWebElement> condition);

        object Until(Func<IWebDriver, IAlert> func);
    }
}