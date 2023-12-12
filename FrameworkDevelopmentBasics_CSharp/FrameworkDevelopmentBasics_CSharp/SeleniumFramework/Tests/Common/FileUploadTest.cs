using NUnit.Framework;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Tests.Common
{
    public class FileUploadTest : TestSuiteFixture
    {
        [Test]
        public void FilesUploadTest()
        {
            var FileUploadPage = PageObjectReg.CommonPages.FileUploadPage;

            Step("File Upload");
            FileUploadPage.FileUpload("Upload.txt");
        }
    }
}