using Autofac;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Common;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.PageUtils;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Base
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