using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements.Base;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Common
{
    public class FileUploadPageLocators
    {
        public Button ChooseFile => new Button(Locators.XPATH, "//input[@id='file-upload']");

        public Button Upload => new Button(Locators.XPATH, "//input[@id='file-submit']");
    }
}