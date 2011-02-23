using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLike.Extensions
{
    public class AwaiterWithCancellation<T> : IAwaiter<T>
    {
  

        private readonly IAwaiter<T> _target;
        private readonly CancellationToken _token;
        private bool _wasCancelled;

        public AwaiterWithCancellation(IAwaiter<T> target, CancellationToken token )
        {
            _target = target;
            _token = token;
        }

        public bool BeginAwait(Action continuation)
        {
            if (!_target.BeginAwait(continuation))
                return false;

            _token.Register(() =>
                                {
                                    Action action;
                                    if (_target.TryCancelAwait(out action))
                                    {
                                        _wasCancelled = true;
                                        action();
                                    }
                                });
            
            return true;
        }

        public T EndAwait()
        {
            if(_wasCancelled)
                throw new TaskCanceledException();
          
                
            return _target.EndAwait();
        }

        public bool TryCancelAwait(out Action continuation)
        {
            return _target.TryCancelAwait(out continuation);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return this;
        }
    }
}
