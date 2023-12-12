using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebdriverWait
{
    public class Wait : IWait
    {
        private readonly WebDriverWait _wait;

        public Wait(IWebDriver Driver, int FromSeconds = 90)
        {
            _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(FromSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            _wait.IgnoreExceptionTypes(
                typeof(NoSuchElementException),
                typeof(ElementNotVisibleException),
                typeof(StaleElementReferenceException)
                );
        }

        public bool Until(Func<IWebDriver, bool> condition)
        {

            return _wait.Until(condition);
        }

        public IWebElement Until(Func<IWebDriver, IWebElement> condition)
        {
            return _wait.Until(condition);
        }

        public object Until(Func<IWebDriver, IAlert> func)
        {
            return _wait.Until(func);
        }

        public IWebDriver Until(Func<IWebDriver, IWebDriver> condition)
        {
            return _wait.Until(condition);
        }
    }
}