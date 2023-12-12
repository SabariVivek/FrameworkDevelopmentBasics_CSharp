using Autofac;

namespace FrameworkDevelopmentBasics_CSharp.PlaywrightFramework.Autofac
{
    public class ContainerBase
    {
        public static IContainer? Container { get; protected set; }
    }
}