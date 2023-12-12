using Autofac;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.PageObjects.Common;
using FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.PageUtils;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.PageObjects.Base
{
    public class PageObjectRegister : TestWebPage
    {
        private readonly IComponentContext _context;

        public PageObjectRegister(IComponentContext context) : base(context)
        {
            _context = context;
        }

        public CommonPages CommonPages => _context.Resolve<CommonPages>();
    }
}