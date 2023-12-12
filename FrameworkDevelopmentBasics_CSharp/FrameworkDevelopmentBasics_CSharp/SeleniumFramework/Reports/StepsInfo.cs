using AventStack.ExtentReports;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.PageUtils;
using NUnit.Framework;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Reports
{
    public class StepsInfo
    {
        public readonly BrowserService BrowserService;

        public StepsInfo()
        {
            BrowserService = new BrowserService();
        }

        public void Step(string Description)
        {
            TestStepPassed(Description);
        }

        public void PrintRunConsole(string Description)
        {
            TestContext.WriteLine(Description);
            TestContext.Progress.WriteLine(Description);
        }

        public void TestStepInfo(string Description)
        {
            ExtentTestManager.GetTest().Info(Description);
            this.PrintRunConsole(Description);
        }

        public void TestStepPassed(string Description)
        {
            ExtentTestManager.GetTest().Pass(Description);
            this.PrintRunConsole(Description);
        }

        public void TestStepFailed(string Description)
        {
            string screenShotPath = ExtentManager.Capture(BrowserService.Driver(), TestContext.CurrentContext.Test.Name);
            ExtentTestManager.GetTest().Fail(Description, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShotPath).Build());
            this.PrintRunConsole(Description);
        }

        public void TestStepSkipped(string Description)
        {
            ExtentTestManager.GetTest().Skip(Description);
            this.PrintRunConsole(Description);
        }
    }
}