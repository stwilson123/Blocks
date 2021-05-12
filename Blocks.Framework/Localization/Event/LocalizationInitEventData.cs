using Blocks.Framework.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Framework.Localization.Event
{
    public class LocalizationInitEventData : DomainEventData
    {
        public IList<string> SourceNames;

    }
 
}
