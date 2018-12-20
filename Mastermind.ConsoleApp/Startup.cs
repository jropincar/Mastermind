using Mastermind.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mastermind.ConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastermind.ConsoleApp
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            var mastermindConfig = new MastermindConfig();
            Configuration.GetSection("MastermindConfig").Bind(mastermindConfig);
            serviceCollection.AddSingleton(mastermindConfig);
            serviceCollection.AddSingleton<IMastermind, Mastermind>();
        }
    }
}
