using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.WebElements.Base;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.WebElements
{
    public class Input : ElementBase
    {
        public Input(Locators Selector, string SelectorValue) : base(Selector, SelectorValue)
        {

        }

        public async Task TypeTextAsync(string value)
        {
            var Element = GetWebElement();
            await Element.ClearAsync();
            await Element.FillAsync(value);
        }
    }
}