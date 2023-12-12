using Autofac;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Autofac;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.TestConfigurations;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Base
{
    public class BasePage
    {
        public By? ElementVisibleAfterLoad { get; set; }

        public void GoToPage()
        {
            Console.WriteLine("<<<<< Browser Launching >>>>>");
            Driver.Navigate().GoToUrl(Configuration.BaseQAUrl);

            if (ElementVisibleAfterLoad != null)
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
                wait.Until(driver => driver.FindElement(ElementVisibleAfterLoad));
            }
        }

        public IWebDriver Driver => ContainerBase.Container.Resolve<IWebDriver>();

        protected ITestConfiguration Configuration => ContainerBase.Container.Resolve<ITestConfiguration>();
    }
}