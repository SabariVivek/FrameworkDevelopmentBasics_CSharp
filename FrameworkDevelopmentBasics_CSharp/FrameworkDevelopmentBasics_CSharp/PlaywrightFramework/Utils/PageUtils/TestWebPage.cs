using Autofac;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.PageUtils
{
    public class TestWebPage
    {
        private readonly IComponentContext _context;

        public TestWebPage(IComponentContext context)
        {
            _context = context;
        }
    }
}