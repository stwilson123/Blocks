using System;
using Abp.Events.Bus;

namespace Blocks.Framework.Event
{
    [Serializable]
    public abstract class DomainEventData : EventData,IDomainEventData
    {
        
    }
 
}