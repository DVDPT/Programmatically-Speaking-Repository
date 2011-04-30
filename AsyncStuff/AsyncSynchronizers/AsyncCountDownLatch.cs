using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using AsyncLike.Extensions;

namespace Serie2.AsyncLike
{
    public class AsyncCountDownLatch
    {

        private const int COMPLETED = 1;
        private const int CANCELLED = 2;
        private const int RUNNING = 3;

        private struct CountDownLatchElement
        {
            public volatile int State;
            public readonly Action Continuation;

            public CountDownLatchElement(Action continuation)
            {
                Continuation = continuation;
                State = RUNNING;
            }
        }


        internal class AsyncCountDownLatchAwaiter : IAwaiter<bool>
        {
            private readonly AsyncCountDownLatch _cdl;
            private CountDownLatchElement _thisElement;
            public AsyncCountDownLatchAwaiter(AsyncCountDownLatch cdl)
            {
                _cdl = cdl;
            }
            public bool BeginAwait(Action continuation)
            {
                if (_cdl._currentCount == 0)
                    return false;

                _thisElement = new CountDownLatchElement(continuation);
                
                _cdl._continuations.Enqueue(_thisElement);
                
                if (_cdl._currentCount == 0 && Interlocked.CompareExchange(ref _thisElement.State,COMPLETED,RUNNING) == RUNNING)
                {
                    return false;
                }

                return true;

            }

            public bool EndAwait()
            {
                return _thisElement.State == COMPLETED;
            }

            public bool TryCancelAwait(out Action continuation)
            {
                if (_thisElement.State == RUNNING && Interlocked.CompareExchange(ref _thisElement.State, CANCELLED, RUNNING) == RUNNING)
                {
                    continuation = _thisElement.Continuation;
                    return true;
                }
                continuation = null;
                return false;

            }

            public IAwaiter<bool> GetAwaiter()
            {
                return this;
            }
        }

        private readonly int _initialCount;
        private volatile int _currentCount;
        private ConcurrentQueue<CountDownLatchElement> _continuations = new ConcurrentQueue<CountDownLatchElement>();

        public AsyncCountDownLatch(int initial)
        {
            _initialCount = _currentCount = initial;
        }

        private bool DecrementCounter(int signalCount)
        {
            int newCount;
            do
            {
                var currentCount = _currentCount;
                if (currentCount == 0)
                    return false;

                newCount = currentCount - signalCount;
                if (newCount < 0)
                    newCount = 0;

                if (Interlocked.CompareExchange(ref _currentCount, newCount, currentCount) == currentCount)
                    break;

            } while (true);

            if (newCount == 0)
                return true;

            return false;
        }

        public bool CountDown(int signalCount = 1)
        {
            if (DecrementCounter(signalCount))
            {
                do
                {
                    CountDownLatchElement element;
                    if (_continuations.TryDequeue(out element))
                    {
                        if(element.State == RUNNING && 
                                Interlocked.CompareExchange(ref element.State,COMPLETED,RUNNING) == RUNNING)
                            TaskEx.Run(element.Continuation);
                    }

                } while (!_continuations.IsEmpty);
                return true;
            }
            return false;
        }

        public void CountUp(int signalCount = 1)
        {
            do
            {
                var currentCount = _currentCount;
                if (currentCount == 0) return;

                var newCount = currentCount + signalCount;
                if (Interlocked.CompareExchange(ref _currentCount, newCount, currentCount) == currentCount)
                    return;

            } while (true);
        }

        public IAwaiter<bool> Wait()
        {
            return new AsyncCountDownLatchAwaiter(this);
        }
    }
}
