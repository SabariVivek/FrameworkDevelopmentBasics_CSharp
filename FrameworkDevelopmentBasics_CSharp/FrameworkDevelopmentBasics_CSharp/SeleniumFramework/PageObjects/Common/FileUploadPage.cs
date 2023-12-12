using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Base;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.PageUtils;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Common
{
    public class FileUploadPage : BasePage, IPageSection
    {
        public readonly FileUploadPageLocators obj;
        public BrowserService browserService;

        public FileUploadPage()
        {
            obj = new FileUploadPageLocators();
            browserService = new BrowserService();
        }

        public void FileUpload(String FileName)
        {
            GoToPage();
            Driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/upload");

            // Upload Section...
            obj.ChooseFile.JSClick();
            browserService.UploadFile(FileName);
            obj.Upload.Click();
        }
    }
}