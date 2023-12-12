using Autofac;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Base;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Common
{
    public class CommonPages : IPageSection
    {
        private readonly IComponentContext _context;

        public CommonPages(IComponentContext context)
        {
            _context = context;
        }

        public LoginPage LoginPage => _context.Resolve<LoginPage>();
        public FileUploadPage FileUploadPage => _context.Resolve<FileUploadPage>();
    }
}