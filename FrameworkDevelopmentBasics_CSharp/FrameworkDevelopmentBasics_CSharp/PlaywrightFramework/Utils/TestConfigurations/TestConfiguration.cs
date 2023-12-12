namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.TestConfigurations
{
    public class TestConfiguration : ITestConfiguration
    {
        public string BaseQAUrl { get; set; }
        public string Browser { get; set; }
        public bool AcceptInsecureCertificates { get; set; }
        public bool Headless { get; }
    }
}