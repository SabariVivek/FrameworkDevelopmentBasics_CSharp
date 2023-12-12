namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.TestConfigurations
{
    public interface ITestConfiguration
    {
        string BaseQAUrl { get; set; }
        string Browser { get; set; }
        bool AcceptInsecureCertificates { get; set; }
    }
}