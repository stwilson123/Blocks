using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Abp.Localization;

namespace Blocks.Framework.Environment.Extensions
{
    public class DefaultCriticalErrorProvider : ICriticalErrorProvider {
        private ConcurrentBag<string> _errorMessages;
        private readonly object _synLock = new object();

        public DefaultCriticalErrorProvider() {
            _errorMessages = new ConcurrentBag<string>();

        }

        public IEnumerable<string> GetErrors() {
            return _errorMessages;
        }

        public void RegisterErrorMessage(string message) {
            if (_errorMessages != null){  //&& _errorMessages.All(m => m.TextHint != message.TextHint)) {
                _errorMessages.Add(message);
            }
        }

        public void Clear() {
            lock (_synLock) {
                _errorMessages = new ConcurrentBag<string>();
            }

        }
    }
}