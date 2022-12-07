using EventBus.Library.Models;
using System;

namespace Models.Library.Events
{
    public abstract class BaseOrderEvent : BaseEvent
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Product { get; set; }
    }
}
