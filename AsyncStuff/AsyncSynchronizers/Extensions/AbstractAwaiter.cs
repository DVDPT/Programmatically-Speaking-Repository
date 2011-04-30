using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serie2.AsyncLike.Extensions;

namespace Serie2.AsyncLike
{
    public abstract class AbstractAwaiter<T>
    {
        public abstract bool BeginAwait(Action continuation);
        public abstract T EndAwait();
        public abstract AbstractAwaiter<T> GetAwaiter();

        protected internal event Func<bool> OnTimeout;
        protected internal Action Continuation { get;  set; }
        protected internal bool TriggerOnTimeoutEvent()
        {
            if(OnTimeout != null)
                return OnTimeout();
            return false;
        }
        



    }
}
