using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncLike.Extensions
{
    public interface IAwaiter<out T>
    {
        bool BeginAwait(Action continuation);
        T EndAwait();
        bool TryCancelAwait(out Action continuation);
        IAwaiter<T> GetAwaiter();
    }
}
