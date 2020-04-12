using Microsoft.Extensions.DependencyInjection;
using CIM.Asset.Parser.XmiParse;

namespace CIM.Asset.Parser.Internal
{
    public static class Config
    {
        public static ServiceProvider Configure()
        {
            var serviceProvider = BuildServiceProvider();

            return serviceProvider;
        }

        private static ServiceProvider BuildServiceProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<Startup>()
                .AddTransient<IXmlTextReaderFactory, XmlTextReaderFactory>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}