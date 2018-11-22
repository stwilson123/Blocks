using System;
using Blocks.Framework.Caching;

namespace Blocks.Framework.Services
{
    public class Clock: IClock {
        public DateTime Now {
            get { return DateTime.Now; }
        }

        public IVolatileToken When(TimeSpan duration) {
            return new AbsoluteExpirationToken(this, duration);
        }

        public IVolatileToken WhenUtc(DateTime absoluteUtc) {
            return new AbsoluteExpirationToken(this, absoluteUtc);
        }

        public class AbsoluteExpirationToken : IVolatileToken {
            private readonly IClock _clock;
            private readonly DateTime _invalidateUtc;

            public AbsoluteExpirationToken(IClock clock, DateTime invalidateUtc) {
                _clock = clock;
                _invalidateUtc = invalidateUtc;
            }

            public AbsoluteExpirationToken(IClock clock, TimeSpan duration) {
                _clock = clock;
                _invalidateUtc = _clock.Now.Add(duration);
            }

            public bool IsCurrent {
                get {
                    return _clock.Now < _invalidateUtc;
                }
            }
        }
    }
}