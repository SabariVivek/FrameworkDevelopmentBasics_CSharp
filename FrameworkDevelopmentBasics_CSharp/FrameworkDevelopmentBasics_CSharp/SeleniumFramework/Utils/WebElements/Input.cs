using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements.Base;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements
{
    public class Input : ElementBase
    {
        public Input(Locators Selector, string SelectorValue) : base(Selector, SelectorValue)
        {

        }

        public void TypeText(string value)
        {
            IWebElement Element = GetWebElement();
            Wait.Until(ExpectedConditions.ElementToBeClickable(Element)).Click();
            Element.Clear();
            Element.SendKeys(value);
        }
    }
}