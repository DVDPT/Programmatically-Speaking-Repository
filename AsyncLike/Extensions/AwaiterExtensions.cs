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
    }
}
