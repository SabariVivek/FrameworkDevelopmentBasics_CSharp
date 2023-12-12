using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Reflection;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Reports
{
    public class ExtentManager
    {
        private ExtentManager()
        {

        }

        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        static string TestReportPath;

        public static ExtentReports Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        static ExtentManager()
        {
            string RootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            string WORK_SPACE_DIRECTORY = RootDirectory + "\\..\\..\\";

            var htmlReporter = new ExtentSparkReporter(
                WORK_SPACE_DIRECTORY + "\\TestExecution_Reports\\FinalReport.html"
                );

            TestReportPath = WORK_SPACE_DIRECTORY + "TestExecution_Reports";

            htmlReporter.Config.Theme = Theme.Dark;
            htmlReporter.Config.DocumentTitle = "Automation Report";
            htmlReporter.Config.ReportName = "FinalReport";

            Instance.AttachReporter(htmlReporter);
        }



        public static string Capture(IWebDriver driver, string screenShotName)
        {
            string localpath = TestReportPath + "\\Defect_Screenshots\\";
            try
            {
                Thread.Sleep(1000);
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                Directory.CreateDirectory(localpath);
                string finalpth = $"{localpath}{screenShotName}.png";
                localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath);
                TestContext.AddTestAttachment(finalpth, $"Test Case Name :: {screenShotName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return localpath;
        }
    }
}