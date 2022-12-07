using EventBus.Library.Models;
using System;
using System.Threading.Tasks;

namespace EventBus.Library
{
    public class Subscribtion<TEvent> : ISubscribtion where TEvent : BaseEvent
    {
        private event Func<TEvent, EventArgs, Task> _eventHandler;
        public Token Token { get; }

        public Subscribtion(Func<TEvent, EventArgs, Task> eventHandler, Token token)
        {
            _eventHandler = eventHandler;
            Token = token;
        }

        public async Task Publish(BaseEvent evnt, EventArgs args)
        {
            await _eventHandler((TEvent)evnt, args);
        }
    }
}
