using Mastermind.ConsoleApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Mastermind.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            var startup = new Startup();

            startup.ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var mastermind = serviceProvider.GetService<IMastermind>();
            mastermind.PlayMastermind();
        }
    }
}
