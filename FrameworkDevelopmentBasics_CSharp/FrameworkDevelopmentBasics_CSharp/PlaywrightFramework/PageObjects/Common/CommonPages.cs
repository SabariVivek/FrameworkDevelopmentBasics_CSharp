using Autofac;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.PageObjects.Base;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.PageObjects.Common
{
    public class CommonPages : IPageSection
    {
        private readonly IComponentContext _context;

        public CommonPages(IComponentContext context)
        {
            _context = context;
        }

        public LoginPage LoginPage => _context.Resolve<LoginPage>();
    }
}