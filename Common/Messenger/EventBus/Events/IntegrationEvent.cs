using System;

namespace Messenger.EventBus.Events
{
    public abstract class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public abstract string EventName { get; }
    }
}