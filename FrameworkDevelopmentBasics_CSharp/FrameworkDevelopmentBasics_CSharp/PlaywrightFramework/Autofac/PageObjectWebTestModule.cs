using Autofac;
using Module = Autofac.Module;
using System.Reflection;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.PageObjects.Base;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.PageUtils;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.TestConfigurations;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Resources;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.WebElements.Base;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Autofac
{
    public class PageObjectWebTestModule : Module
    {
        private readonly ITestConfiguration _configuration;

        public PageObjectWebTestModule(ITestConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var testassembly = Assembly.GetAssembly(typeof(PageObjectRegister))!;

            builder.RegisterAssemblyTypes(testassembly).Where(t => typeof(ElementBase).IsAssignableFrom(t))
                .InstancePerDependency().AsSelf();

            builder.RegisterAssemblyTypes(testassembly).Where(t => typeof(IPageSection).IsAssignableFrom(t))
               .InstancePerLifetimeScope().AsSelf();

            builder.RegisterAssemblyTypes(testassembly).Where(t => typeof(IPages).IsAssignableFrom(t))
                .InstancePerLifetimeScope().AsSelf();

            builder.RegisterAssemblyTypes(testassembly).Where(t => typeof(TestWebPage).IsAssignableFrom(t))
                .InstancePerLifetimeScope().AsSelf();

            builder.RegisterType<TestDataService>().AsSelf().SingleInstance();
        }
    }
}