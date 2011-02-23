using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Serie2.AsyncLike.Extensions
{
    public static class AwaiterExtensions
    {
        public static AbstractAwaiter<T> WithTimeout<T>(this AbstractAwaiter<T> _this, int milis) 
        {

            if (milis == Timeout.Infinite || milis < 0)
                return _this;

            return new TimeoutAwaiter<T>(_this, milis);
        }

        public static IAwaiter<T> WithTimeout<T>(this IAwaiter<T> _this, int timeout)
        {
            if (timeout == Timeout.Infinite || timeout < 0)
            {
                return _this;
            }

            return new AwaiterWithTimer<T>(_this, timeout);
        }

        public static IAwaiter<T> WithCancellation <T>(this IAwaiter<T> _this, CancellationToken token)
        {
            return new AwaiterWithCancellation<T>(_this, token);
        }
    }
}
