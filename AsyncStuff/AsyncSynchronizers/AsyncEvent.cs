using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serie2.AsyncLike.Extensions;

namespace AsyncLike
{
    public class AsyncEvent
    {
        private const int WAITING = 0x0;
        private const int SETTED = 0x2;
        private const int CANCELLED = 0x3;

        private readonly ConcurrentQueue<EventAwaiter> _tasks = new ConcurrentQueue<EventAwaiter>();
        private readonly bool _isAutoReset;

        private volatile bool _isSet;
        
        private class EventAwaiter : IAwaiter
        {
           
            private readonly AsyncEvent _event;
            internal Action _continuation;
            internal volatile int _state;

            public EventAwaiter(AsyncEvent @event)
            {
                _event = @event;
            }

            public bool BeginAwait(Action continuation)
            {
                _state = WAITING;
                if (_event._isSet)
                {
                    _state = SETTED;
                    return false;
                }
                _continuation = continuation;
                _event._tasks.Enqueue(this);
                return true;
            }

            public void EndAwait()
            {
                Debug.Assert(_state == SETTED);
            }

            public bool TryCancelAwait(out Action continuation)
            {
                if (_state == WAITING && Interlocked.CompareExchange(ref _state, CANCELLED, WAITING) == WAITING)
                {
                    continuation = _continuation;
                    return true;
                }
                continuation = null;
                return false;
            }

            public IAwaiter GetAwaiter()
            {
                return this;
            }
        }


        public AsyncEvent(bool initialState, bool autoReset)
        {
            _isSet = initialState;
            _isAutoReset = autoReset;
        }

        private void DoSet()
        {
            _isSet = true;
            while (_tasks.Count != 0)
            {
                EventAwaiter eventAwaiter;
                if (_tasks.TryDequeue(out eventAwaiter))
                {
                    if (eventAwaiter._state == WAITING && Interlocked.CompareExchange(ref eventAwaiter._state, SETTED, WAITING) == WAITING)
                        TaskEx.Run(eventAwaiter._continuation);
                }
            }
        }
      

        public IAwaiter Wait()
        {
            return new EventAwaiter(this);
        }

        public void Set()
        {
            DoSet();
            if(_isAutoReset)
                Reset();
        }

        

        public void Reset()
        {
            _isSet = false;
        }
    }
}
