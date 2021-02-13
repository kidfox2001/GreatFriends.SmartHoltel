using Microsoft.Extensions.DependencyInjection;
using System;

namespace GreatFriends.SmartHoltel.PayWithDI
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new ServiceCollection();

            ConfigureServices(service);

            var provider = service.BuildServiceProvider();

            provider.GetService<ICounter>().Print();
            provider.GetService<ICounter>().Print();

            using (var scope = provider.CreateScope())
            {
                var provider2 = scope.ServiceProvider;
                provider2.GetService<ICounter>().Print();
                provider2.GetService<ICounter>().Print();
            }

            provider.GetService<ICounter>().Print();
            provider.GetService<ICounter>().Print();

        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // service.AddSingleton<ICounter, Counter>();  // คลาสนี้มี object เดียวไม่มีทางมีสอง object
            //service.AddTransient<ICounter, Counter>();  // new object ทุกครั้งที่ขอ
            services.AddScoped<ICounter, Counter>();  // (Singleton per scope)
            services.AddScoped<IOutput, ConsoleOutput>();
            services.AddScoped<IOutput, SpeakerOutput>();
        }
    }
}


