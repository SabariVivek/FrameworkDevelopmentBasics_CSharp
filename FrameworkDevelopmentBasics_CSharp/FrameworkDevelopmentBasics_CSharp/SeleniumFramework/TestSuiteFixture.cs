using Autofac;
using AventStack.ExtentReports;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Base;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Reports;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Resources;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Resources.User;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework
{
    [TestFixture]
    public class TestSuiteFixture : TestFixtureBase
    {
        protected PageObjectRegister PageObjectReg { get; private set; }
        public static ExtentTest childTest;
        public readonly StepsInfo stepsInfo = new StepsInfo();
        protected UserType Data { get; private set; }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("Running: {0}", TestContext.CurrentContext.Test.MethodName);
            Console.WriteLine("<<<<< Test Execution - Start >>>>>");

            PageObjectReg = Container.Resolve<PageObjectRegister>();
            Data = TestFixtureBase.Container.Resolve<TestDataService>().Data;

            // Extent Report Configurations...
            childTest = ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            var category = (string)TestContext.CurrentContext.Test.Properties.Get("Category")!;
            childTest.AssignCategory(category);

            TestStepInfo("<<<<< Test Execution - Start >>>>");
            TestStepInfo($"Test Case Name - {TestContext.CurrentContext.Test.Name}");
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Console.WriteLine("<<<<< Test Execution - End >>>>>");

                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = " " + TestContext.CurrentContext.Result.StackTrace + " ";
                Status logstatus;

                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        TestStepFailed("Test ended with " + logstatus + stacktrace);
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        TestStepSkipped("Test ended with " + logstatus + stacktrace);
                        break;
                    case TestStatus.Passed:
                        logstatus = Status.Pass;
                        TestStepPassed("Test ended with " + logstatus + stacktrace);
                        break;
                    default:
                        throw new Exception("Error on validate the test status");
                }
                TestStepInfo("<<<<< Test Execution - End >>>>>");
            }
            catch (Exception ex)
            {
                throw new Exception($"Test script failed due to :: {ex}");
            }
        }

        public void Step(string Description)
        {
            stepsInfo.TestStepPassed(Description);
        }

        public void TestStepInfo(string Description)
        {
            stepsInfo.TestStepInfo(Description);
        }

        public void TestStepPassed(string Description)
        {
            stepsInfo.TestStepPassed(Description);
        }

        public void TestStepFailed(string Description)
        {
            stepsInfo.TestStepFailed(Description);
        }

        public void TestStepSkipped(string Description)
        {
            stepsInfo.TestStepSkipped(Description);
        }
    }
}