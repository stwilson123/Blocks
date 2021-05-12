using Blocks.Framework.Domain.Uow;
using Blocks.Framework.Event;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Blocks.Framework.Localization.Event
{
   [UnitOfWork(TransactionScopeOption.Suppress)]
    public class LocalizationInitEvent : IDomainEventHandler<LocalizationInitEventData>
    {
        private ILocalizationManager localizationManager;

        public LocalizationInitEvent(ILocalizationManager localizationManager)
        {
            this.localizationManager = localizationManager;
        }
        public void HandleEvent(LocalizationInitEventData eventData)
        {
            this.localizationManager.Initialize();
        }
    }
}
