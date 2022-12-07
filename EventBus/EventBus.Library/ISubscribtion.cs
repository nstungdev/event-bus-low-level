using EventBus.Library.Models;
using System;
using System.Threading.Tasks;

namespace EventBus.Library
{
    public interface ISubscribtion
    {
        Token Token { get; }
        Task Publish(BaseEvent evnt, EventArgs args);
    }
}
