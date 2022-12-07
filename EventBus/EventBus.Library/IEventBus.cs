using EventBus.Library.Models;
using System;
using System.Threading.Tasks;

namespace EventBus.Library
{
    public interface IEventBus
    {
        Token Subscribe<TEvent>(Func<TEvent, EventArgs, Task> eventHandler) where TEvent : BaseEvent;
        Task Publish<TEvent>(TEvent evnt, EventArgs args) where TEvent : BaseEvent;
        void UnSubscribe<TEvent>(string tokenId) where TEvent : BaseEvent;
    }
}
