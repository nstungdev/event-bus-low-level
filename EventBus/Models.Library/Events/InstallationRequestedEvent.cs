using System;

namespace Models.Library.Events
{
    public class InstallationRequestedEvent : BaseOrderEvent
    {
        public DateTime InstallationTimeRequested { get; set; }
    }
}
