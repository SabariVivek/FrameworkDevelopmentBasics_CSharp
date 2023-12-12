using Autofac;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Autofac;
using Microsoft.Playwright;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.WebElements.Base
{
    public class ElementBase
    {
        public IPage Driver => ContainerBase.Container.Resolve<IPage>();

        public string Selector { get; set; }

        public ILocator WebElement { get; set; }

        public ElementBase(Locators Selector, string SelectorValue)
        {
            if (Selector == Locators.XPATH)
            {
                this.Selector = SelectorValue;
            }
            else if (Selector == Locators.CSS)
            {
                this.Selector = SelectorValue;
            }
            else if (Selector == Locators.ID)
            {
                this.Selector = SelectorValue;
            }
            else
            {
                throw new NotImplementedException($"Selector: {Selector} not handled in Implementation");
            }
        }

        public string GetAttribute(string value) => GetWebElement().GetAttributeAsync(value).Result!;

        public string Text => GetWebElement().TextContentAsync().Result!;

        public ILocator GetWebElement()
        {
            return GetWebElement(Selector);
        }

        public async Task<IFrame> SwitchToFrame()
        {
            try
            {
                var iframeElement = await Driver.QuerySelectorAsync(Selector);
                var Frame = await iframeElement.ContentFrameAsync();
                return Frame;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        protected ILocator GetWebElement(string Selector)
        {
            WebElement = Driver.Locator(Selector);
            return WebElement;
        }

        public async Task Click()
        {
            try
            {
                var element = GetWebElement();
                await element.ClickAsync();
            }
            catch (PlaywrightException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public async Task AssertIsVisible()
        {
            try
            {
                var element = GetWebElement(Selector);
                await Driver.Locator(Selector).IsVisibleAsync();
            }
            catch (PlaywrightException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool AssertExists()
        {
            try
            {
                WebElement = Driver.Locator(Selector);
                if (WebElement != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AssertNotExists()
        {
            try
            {
                await Driver.Locator(Selector).IsHiddenAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ElementExists()
        {
            return await Driver.Locator(Selector).CountAsync() > 0;
        }

        public async Task<bool> ElementNotExists()
        {
            return await Driver.Locator(Selector).CountAsync() == 0;
        }

        public async Task<bool> ElementDisplayed()
        {
            return await Driver.Locator(Selector).IsVisibleAsync();
        }

        public async Task<int> ElementCount()
        {
            return await Driver.Locator(Selector).CountAsync();
        }

        public async Task<string> Getattribute(string attibutename)
        {
            return await Driver.GetAttributeAsync(Selector, attibutename);
        }

        public async Task AssertIsNotVisible()
        {
            try
            {
                var selelement = Driver.Locator(Selector);
                await selelement.IsHiddenAsync();
            }
            catch (Exception)
            {
                Assert.Fail($"Expected element {Selector?.ToString()} failed to be not visible after waiting 60 seconds on {Driver.Url}");
            }
        }

        public async Task RightClick()
        {

            await Driver.GetByText(Selector).ClickAsync(new() { Button = MouseButton.Right });

        }

        public async Task WaitForElementToVisible()
        {
            Thread.Sleep(6000);
            try
            {
                var element = GetWebElement(Selector);
                await element.WaitForAsync();
                await element.IsVisibleAsync();
            }
            catch (Exception)
            {
                Assert.Fail($"Expected element {Selector?.ToString()} failed to be visible after waiting 60 seconds on {Driver.Url}");
            }
        }

        public async Task ScrollIntoElementView()
        {
            await Driver.Locator(Selector).ScrollIntoViewIfNeededAsync();
        }

        public async Task HoverOnElement()
        {
            var LookupElement = GetWebElement(Selector);
            await LookupElement.WaitForAsync();
            await LookupElement.HoverAsync();
        }

        public async Task HoverOnElementAndClick()
        {
            try
            {
                var LookupElement = GetWebElement(Selector);
                await LookupElement.HoverAsync();
                await LookupElement.ClickAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void JSClick(IWebElement Element)
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)Driver;
            if (Element != null)
            {
                javaScriptExecutor.ExecuteScript("arguments[0].click();", Element);
            }
            else
            {
                WebElement = GetWebElement();
                javaScriptExecutor.ExecuteScript("arguments[0].click();", WebElement);
            }
        }

        public IList<IElementHandle> GetWebElements()
        {
            IList<IElementHandle> elements = (IList<IElementHandle>)Driver.QuerySelectorAllAsync(Selector);
            return elements;
        }
    }
}