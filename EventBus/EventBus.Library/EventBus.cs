using EventBus.Library.Models;
using EventBus.Library.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBus.Library
{
    public class EventBus : IEventBus
    {
        private readonly IDictionary<Type, List<ISubscribtion>> _subscriptions;
        private readonly ITokenUtils _tokenUtils;
        public EventBus(ITokenUtils tokenUtils) 
        {
            _subscriptions = new Dictionary<Type, List<ISubscribtion>>();
            _tokenUtils = tokenUtils;
        }

        public Token Subscribe<TEvent>(Func<TEvent, EventArgs, Task> eventHandler) where TEvent : BaseEvent
        {
            var token = _tokenUtils.GenerateNewToken();
            var subscription = new Subscribtion<TEvent>(eventHandler, token);

            if (!_subscriptions.ContainsKey(typeof(TEvent)))
            {
                _subscriptions.Add(typeof(TEvent), new List<ISubscribtion>() { subscription });
            }
            else
            {
                _subscriptions[typeof(TEvent)].Add(subscription);
            }

            return token;
        }

        public async Task Publish<TEvent>(TEvent evnt, EventArgs args) where TEvent : BaseEvent
        {
            var allSubscribtions = _subscriptions?[typeof(TEvent)];

            foreach (var subscribtion in allSubscribtions)
            {
                await subscribtion.Publish(evnt, args);
            }
        }

        public void UnSubscribe<TEvent>(string tokenId) where TEvent : BaseEvent
        {
            var subscription = _subscriptions[typeof(TEvent)].FirstOrDefault(x => x.Token.TokenId == tokenId);
            if (subscription != null)
                _subscriptions[typeof(TEvent)].Remove(subscription);
        }
    }
}
