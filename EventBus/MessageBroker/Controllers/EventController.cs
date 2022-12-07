using EventBus.Library;
using EventBus.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Library;
using Models.Library.Events;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MessageBroker.Controllers
{
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventBus _eventBus;

        public EventController(ILogger<EventController> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        [HttpPost]
        [Route("Subscribe")]
        public Token Subscribe(SubscribePayload subscribePayload)
        {
            if (subscribePayload.Type == typeof(AddressChangeEvent).Name)
                return _eventBus.Subscribe<AddressChangeEvent>(GetEventHandler<AddressChangeEvent>(subscribePayload.Callback));
            else if (subscribePayload.Type == typeof(InstallationRequestedEvent).Name)
                return _eventBus.Subscribe<InstallationRequestedEvent>(GetEventHandler<InstallationRequestedEvent>(subscribePayload.Callback));

            return null;
        }

        [HttpPost]
        [Route("Publish")]
        public async Task Publish(PublishPaload publishPaload)
        {
            if (publishPaload.Type == typeof(AddressChangeEvent).Name)
                await _eventBus.Publish<AddressChangeEvent>(GetEvent<AddressChangeEvent>(publishPaload.Event), new BaseOrderEventArgs());
            else if (publishPaload.Type == typeof(InstallationRequestedEvent).Name)
                await _eventBus.Publish<InstallationRequestedEvent>(GetEvent<InstallationRequestedEvent>(publishPaload.Event), new BaseOrderEventArgs());
        }

        private Func<object, EventArgs, Task> GetEventHandler<T>(Callback callback) where T : BaseOrderEvent
        {
            return async (object? sender, EventArgs e) => {
                try
                {
                    var callbackManager = new CallbackManager(callback);
                    var response = await callbackManager.CallAsync<T>((T)sender);

                    if (response.Value != System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine($"Failed to call service for orderId {((T)sender).Id}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to call service for orderId {((T)sender).Id}, {ex.Message}");
                }
            };
        }

        private TEvent GetEvent<TEvent>(string payload)
        {
            return JsonConvert.DeserializeObject<TEvent>(payload);
        }
    }
}
