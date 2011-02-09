using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Serie2.AsyncLike.Extensions
{
    public class TimeoutAwaiter<E> : AbstractAwaiter<E>
    {

        private const int COMPLETED = 1;
        private const int TIMEOUT = 2;
        private const int NOT_COMPLETED = 0;

        private readonly AbstractAwaiter<E> _target;
        private readonly int _timeout;

        private volatile int _state;
        private Timer _timer;
        private bool _timeoutAborted;
        private bool _continuationCalled;
        


        public TimeoutAwaiter(AbstractAwaiter<E> target, int timeout)
        {
            _target = target;
            _timeout = timeout;
            _state = NOT_COMPLETED;




        }


        private bool ChangeAwaiterStateTo(int newState)
        {
            return _state == NOT_COMPLETED && Interlocked.CompareExchange(ref _state, newState, NOT_COMPLETED) == NOT_COMPLETED; ;
        }

        public override bool BeginAwait(Action continuation)
        {
            //
            //  Save the real continuation on this object
            //
            Continuation = continuation;
            //
            //  Save the timer continuation on target
            //
            _target.Continuation =
                () =>
                {
                    if (ChangeAwaiterStateTo(COMPLETED))
                    {
                        //
                        //  The work was done before timeout
                        //  Call the real continuation
                        //
                        if (Continuation != null)
                            Continuation();
                        return;

                    }
                    //
                    //  
                    //
                    lock (this)
                    {
                        //
                        //  Only when the flags timeoutAborted and continuationCalled are setted to true, we call the real continuation
                        //  this guarantees that the async operation was already completed when the timeout was cancelled.
                        //  When the timeout is not aborted this code isn't needed
                        //
                        if (_timeoutAborted && _continuationCalled)
                        { 
                            Continuation();
                        }
                        _continuationCalled = true;

                    }
                };
            //
            //  if the target returns right away dont start the timer
            //
            if (!_target.BeginAwait(_target.Continuation) && ChangeAwaiterStateTo(COMPLETED))
            {
                //
                //  The operation was been completed
                //
                return false;
            }

            _timer = new Timer(_ =>
            {
                if (ChangeAwaiterStateTo(TIMEOUT))
                {
                    
                    //
                    //  Call the timeout event to do cleanup
                    //
                    if (_target.TriggerOnTimeoutEvent())
                    {
                        //
                        //  The operation was succeeded at the same time that the timer expired so assert the state to COMPLETED 
                        //  Set continuation to null, to avoid continuation to be called two times
                        //
                        lock (this)
                        {
                            _timeoutAborted = true;
                        }   
                        Interlocked.Exchange(ref _state, COMPLETED);

                        //
                        //  Call the timer continuation to be sure that the async operation already ended
                        //
                        _target.Continuation();
                        return;

                    }

                    //
                    //  Call the real continuation.
                    //
                    Continuation();

                }

                _timer.Dispose();
            });

            //
            //  launch the timer
            //
            _timer.Change(_timeout, Timeout.Infinite);
            return true;

        }



        public override E EndAwait()
        {
            Debug.Assert(_state != NOT_COMPLETED);

            if (_state == COMPLETED)
            {
                return _target.EndAwait();
            }
            else
            {
                throw new TimeoutException("Operation Timeout");
            }
        }

        public override AbstractAwaiter<E> GetAwaiter()
        {
            return this;
        }
    }
}
