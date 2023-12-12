using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.WebElements.Base;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.WebElements
{
    public class Button : ElementBase
    {
        public Button(Locators Selector, string SelectorValue) : base(Selector, SelectorValue)
        {

        }

        public async Task ClickAsync()
        {
            try
            {
                var element = GetWebElement();
                await element.ClickAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.StackTrace);
            }
        }
    }
}