namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Utils.TestConfigurations
{
    public interface ITestConfiguration
    {
        string BaseQAUrl { get; set; }
        string Browser { get; set; }
        bool AcceptInsecureCertificates { get; set; }
        bool Headless { get; }
    }
}