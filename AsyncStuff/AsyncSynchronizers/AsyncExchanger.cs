using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Serie2.AsyncLike
{
    public class AsyncExchanger<T> where T:class 
    {

        private ExchangerAwaiter _objToExchange;

        public class ExchangerAwaiter : AbstractAwaiter<T>
        {

            private readonly T _valueToExchange;
            private readonly AsyncExchanger<T> _exchanger;
            private T _returnValue;
            private Action _continuation;

            public ExchangerAwaiter(T valueToExchange, AsyncExchanger<T> exchanger)
            {
                _valueToExchange = valueToExchange;
                _exchanger = exchanger;
                OnTimeout += () =>
                                 {
                                     if (exchanger._objToExchange == this)
                                     {
                                         //
                                         // try clean up
                                         //
                                         if (Interlocked.CompareExchange(ref exchanger._objToExchange, null, this) == this)
                                             return false;
                                     }
                                     //
                                     // The exchange already succeeded
                                     //
                                     return true;
                                 };
            }

            public override bool BeginAwait(Action continuation)
            {
                do
                {
           
                    var awaitedTask = _exchanger._objToExchange;
                    _continuation = continuation;
                    
                    if (awaitedTask != null && Interlocked.CompareExchange(ref _exchanger._objToExchange, null, awaitedTask) == awaitedTask)
                    {
                        
                        //
                        //  There is already some thread waiting to exchange, exchange values
                        //
                        _returnValue = awaitedTask._valueToExchange;
                        awaitedTask._returnValue = _valueToExchange;
                        //
                        //  Schedule the awaitedTask continuation 
                        //
                        
                        if(awaitedTask._continuation != null)
                            Task.Factory.StartNew(awaitedTask._continuation);
                        //
                        //  return false so that the continuation is done without waiting
                        //
                        return false;
                    }

                    //
                    //  If we got here means that no thread is waiting to exchange
                    //
                    if (Interlocked.CompareExchange(ref _exchanger._objToExchange, this, null) == null)
                    {
                        //
                        //  reference published just return
                        //
                        return true;
                    }
                } while (true);
            }

            public override T EndAwait()
            {
                return _returnValue;
            }

            public override AbstractAwaiter<T> GetAwaiter()
            {
              
                return this;
            }
        }

        
        public ExchangerAwaiter Exchange(T t)
        {
            return new ExchangerAwaiter(t, this);
        }
    }
}
