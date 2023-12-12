using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Autofac;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Autofac;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebdriverWait;
using System.Reflection;
using System.Diagnostics;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.PageUtils
{
    public class BrowserService
    {
        public IWait Wait => new Wait(Driver());

        public BrowserService()
        {

        }

        public IWebDriver Driver()
        {
            return ContainerBase.Container.Resolve<IWebDriver>();
        }

        public void SwitchToDefault()
        {
            Driver().SwitchTo().DefaultContent();
        }

        public void MaximizeWindow()
        {
            Driver().Manage().Window.Maximize();
        }

        public void AlertAccept()
        {
            IAlert alert = Driver().SwitchTo().Alert();
            alert.Accept();
        }

        public IAlert Alert()
        {
            Wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = Driver().SwitchTo().Alert();
            return alert;
        }

        public Actions Actions()
        {
            Actions actions = new Actions(Driver());
            return actions;
        }

        public void Refresh()
        {
            Driver().Navigate().Refresh();
        }

        public INavigation Navigate()
        {
            INavigation navigation = Driver().Navigate();
            return navigation;

        }

        public IJavaScriptExecutor JavaScriptExecutor()
        {
            IJavaScriptExecutor JavaScriptExecutor = (IJavaScriptExecutor)Driver();
            return JavaScriptExecutor;
        }

        public void WaitForPageLoad()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Wait.Until(driver => JavaScriptExecutor().ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void UrlContains(string url)
        {
            Wait.Until(ExpectedConditions.UrlContains(url));
        }

        public void TitleContains(string title)
        {
            Wait.Until(ExpectedConditions.TitleContains(title));
        }

        public void CloseCurrentWindow()
        {
            Driver().Close();
        }

        public string GetURL()
        {
            return Driver().Url;
        }

        public void QuitDriver()
        {
            DeleteAllCookies();
            Driver().Quit();
        }

        public void DeleteAllCookies()
        {
            Driver().Manage().Cookies.DeleteAllCookies();
        }

        public void UploadFile(string FileName)
        {
            Thread.Sleep(100);
            string RootDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            string FilePath =
              $"{RootDirectory}\\..\\..\\..\\..\\FrameworkDevelopmentBasics_CSharp\\SeleniumFramework\\Resources\\{FileName}";

            this.RunAutoItFile("File_Upload_ImportPL.exe", FilePath);
            Thread.Sleep(2000);
        }

        public void RunAutoItFile(string AutoItFileName, string FilePath = null)
        {
            string RootDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            string FileUploadExeFilePath =
                $"{RootDirectory}\\..\\..\\..\\..\\FrameworkDevelopmentBasics_CSharp\\SeleniumFramework\\Helpers\\AutoIT\\{AutoItFileName}";
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = FileUploadExeFilePath;

            if (FilePath != null)
            {
                startInfo.Arguments = FilePath;
            }
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}