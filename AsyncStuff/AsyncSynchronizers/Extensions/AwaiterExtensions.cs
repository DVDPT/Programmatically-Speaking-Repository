using System;
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

        public static IAwaiter<object> WrapToGenericAwaiter(this IAwaiter _this)
        {
            return new AwaiterWrapper(_this);
        }

        private class AwaiterWrapper : IAwaiter<object>
        {

            private IAwaiter _target;
            public AwaiterWrapper(IAwaiter target)
            {
                _target = target;
            }

            public bool BeginAwait(Action continuation)
            {
                return _target.BeginAwait(continuation);
            }

            public object EndAwait()
            {
                _target.EndAwait();
                return null;
            }

            public bool TryCancelAwait(out Action continuation)
            {
                return _target.TryCancelAwait(out continuation);
            }

            public IAwaiter<object> GetAwaiter()
            {
                return this;
            }
        }
    }
}
