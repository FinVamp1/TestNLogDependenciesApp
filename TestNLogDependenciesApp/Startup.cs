using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

[assembly: FunctionsStartup(typeof(TestNLogDependenciesApp.Startup))]
namespace TestNLogDependenciesApp
{
    class Startup : FunctionsStartup
    {

        //private static string GetNLogPath() => Path.Combine(Environment.GetEnvironmentVariable("HOME"), @"site\wwwroot\NLog.config");
        private const string ATFLoggerName = "ATF";

        public Startup()
        {

        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(typeof(ILogger), this.GetLogger());
               
        }

        private ILogger GetLogger()
        {
            var configFile = new NLog.Config.XmlLoggingConfiguration("D:\\home\\site\\wwwroot\\Nlog.config");
            var factory = new NLog.LogFactory(configFile);
            NLogLoggerProvider provider = new NLogLoggerProvider(new NLogProviderOptions(), factory);
            return provider.CreateLogger(ATFLoggerName);
        }
    }
}
