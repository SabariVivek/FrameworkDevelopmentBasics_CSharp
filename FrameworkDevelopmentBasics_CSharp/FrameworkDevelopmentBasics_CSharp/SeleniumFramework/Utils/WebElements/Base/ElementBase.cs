using Autofac;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Autofac;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebdriverWait;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements.Base
{
    public class ElementBase : IElementBase
    {
        public IWebDriver Driver => ContainerBase.Container.Resolve<IWebDriver>();
        public By Selector { get; set; }
        public IWebElement WebElement { get; set; }
        public IWait Wait => new Wait(Driver);

        public ElementBase(Locators Selector, string SelectorValue)
        {
            if (Selector == Locators.XPATH)
            {
                this.Selector = By.XPath(SelectorValue);
            }
            else if (Selector == Locators.CSS)
            {
                this.Selector = By.CssSelector(SelectorValue);
            }
            else if (Selector == Locators.ID)
            {
                this.Selector = By.Id(SelectorValue);
            }
            else
            {
                throw new NotImplementedException($"Selector: {Selector} not handled in Implementation");
            }
        }

        public IWebElement GetWebElement()
        {
            return GetWebElement(Selector);
        }

        public void SwitchToFrame()
        {
            Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(Selector));
        }

        public void SwitchToDefault()
        {
            Driver.SwitchTo().DefaultContent();
        }

        protected IWebElement GetWebElement(By selector)
        {
            WebElement = Wait.Until(ExpectedConditions.ElementIsVisible(selector));
            return WebElement;
        }

        public IList<IWebElement> GetWebElements()
        {
            IList<IWebElement> Elements = Driver.FindElements(Selector);
            return Elements;
        }

        public void AssertIsVisible()
        {
            try
            {
                IWebElement element = null!;
                element = Wait.Until(ExpectedConditions.ElementIsVisible(Selector));
                if (element != null)
                {
                    Assert.True(true, " Expected elemet is Visible");
                }
                else
                {
                    Assert.True(false, "Expected elemet is not Visible");
                }
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail($"Expected element {Selector?.ToString()} failed to be visible after waiting 60 seconds on {Driver.Url}");
            }
        }

        public bool AssertExists()
        {
            try
            {
                WebElement = Wait.Until(ExpectedConditions.ElementExists(Selector));
                if (WebElement != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool ElementExists()
        {
            return Driver.FindElements(Selector).Count > 0;
        }

        public bool ElementNotExists()
        {
            return Driver.FindElements(Selector).Count <= 0;
        }

        public bool ElementDisplayed()
        {
            if (Driver.FindElement(Selector).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int ElementCount()
        {
            return Driver.FindElements(Selector).Count;
        }

        public string GetAttribute(string AttributeName)
        {
            return Wait.Until(ExpectedConditions.ElementExists(Selector)).GetAttribute(AttributeName);
        }

        public string GetText()
        {
            return Wait.Until(ExpectedConditions.ElementExists(Selector)).Text;
        }

        public void RightClick()
        {
            Wait.Until(ExpectedConditions.ElementExists(Selector));
            Actions action = new Actions(Driver);
            IWebElement ele = Driver.FindElement(Selector);
            action.ContextClick(ele).Perform();
        }

        public bool ElementIsEnabled()
        {
            var element = Wait.Until(ExpectedConditions.ElementExists(Selector));
            string val = element.GetAttribute("disabled");
            return string.IsNullOrEmpty(val);
        }

        public void WaitForElementToVisible()
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(Selector));
            }
            catch (WebDriverTimeoutException)
            {

                Assert.Fail($"Expected element {Selector?.ToString()} failed to be visible after waiting 60 seconds on {Driver.Url}");
            }
        }

        public void WaitForElementToClickable()
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementToBeClickable(Selector));
            }
            catch (WebDriverTimeoutException)
            {

                Assert.Fail($"Expected element {Selector?.ToString()} failed to be Clickable after waiting 60 seconds on {Driver.Url}");
            }
        }

        public void ScrollIntoElementView(IWebElement Element = null)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            if (Element != null)
            {
                js.ExecuteScript("arguments[0].scrollIntoView();", Element);
            }
            else
            {
                js.ExecuteScript("arguments[0].scrollIntoView();", GetWebElement());
            }
        }

        public void HoverOnElement(IWebElement? Element = null)
        {
            Actions action = new Actions(Driver);
            if (Element != null)
            {
                action.MoveToElement(Wait.Until(ExpectedConditions.ElementToBeClickable(Element))).Perform();
            }
            else
            {
                action.MoveToElement(GetWebElement()).Perform();
            }
        }

        public void HoverOnElementAndClick(IWebElement? Element = null)
        {
            Actions action = new Actions(Driver);
            if (Element != null)
            {
                action.MoveToElement(Element).Click().Build().Perform();
            }
            else
            {
                WebElement = Driver.FindElement(Selector);
                action.MoveToElement(WebElement).Click().Build().Perform();
            }
        }

        public void JSClick(IWebElement? Element = null)
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

        public void DoubleClick(IWebElement? Element = null)
        {
            Actions action = new Actions(Driver);
            if (Element != null)
            {
                action.DoubleClick(Element).Click().Perform();
            }
            else
            {
                action.DoubleClick(GetWebElement()).Click().Perform();
            }
        }

        public void Dropdownlist(string Value)
        {
            IWebElement element = Wait.Until(ExpectedConditions.ElementExists(Selector));
            IWebElement elementconainer = element;
            IList<IWebElement> containerlist = elementconainer.FindElements(By.TagName("li"));
            for (int i = 0; i < containerlist.Count; i++)
            {
                if (containerlist[i].Text.Equals(Value))
                {
                    Wait.Until(ExpectedConditions.ElementToBeClickable(containerlist[i])).Click();
                    break;
                }
            }
        }

        public string SelectDropdownOptionText(string text)
        {
            IWebElement element = Wait.Until(ExpectedConditions.ElementExists(Selector));
            try
            {
                SelectElement dropdown = new SelectElement(element);
                dropdown.SelectByText(text);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message + " on page");
            }
            return text;
        }

        public void Enter()
        {
            Driver.FindElement(Selector).SendKeys(Keys.Enter);
        }

        public string GetCssValue(string property)
        {
            return Wait.Until(ExpectedConditions.ElementExists(Selector)).GetCssValue(property);
        }
    }
}