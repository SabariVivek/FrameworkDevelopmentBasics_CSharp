using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebdriverWait;
using OpenQA.Selenium;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements.Base
{
    public interface IElementBase
    {
        IWebDriver Driver { get; }
        By Selector { get; set; }
        IWebElement WebElement { get; set; }
        IWait Wait { get; }
    }
}