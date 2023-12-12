using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements.Base;
using SeleniumExtras.WaitHelpers;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements
{
    public class Button : ElementBase
    {
        public Button(Locators Selector, string SelectorValue) : base(Selector, SelectorValue)
        {

        }

        public void Click()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(GetWebElement())).Click();
        }
    }
}