using EventBus.Library.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Library
{
    public static class EventBusStartup
    {
        public static void AddEventBus(this IServiceCollection services)
        {
            services.AddSingleton<ITokenUtils, TokenUtils>();
            services.AddSingleton<IEventBus, EventBus>();
        }
    }
}
