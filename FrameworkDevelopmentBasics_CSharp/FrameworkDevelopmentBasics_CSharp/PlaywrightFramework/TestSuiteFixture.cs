using Autofac;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.PageObjects.Base;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Resources;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Resources.User;
using NUnit.Framework;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework
{
    [TestFixture]
    public class TestSuiteFixture : TestFixtureBase
    {
        protected PageObjectRegister PageObjectReg { get; private set; }
        protected UserType Data { get; private set; }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("Running: {0}", TestContext.CurrentContext.Test.MethodName);
            Console.WriteLine("<<<<< Test Execution - Start >>>>>");

            PageObjectReg = Container.Resolve<PageObjectRegister>();
            Data = TestFixtureBase.Container.Resolve<TestDataService>().Data;
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Console.WriteLine("<<<<< Test Execution - End >>>>>");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed On Test script status checking :: {ex}");
            }
        }
    }
}