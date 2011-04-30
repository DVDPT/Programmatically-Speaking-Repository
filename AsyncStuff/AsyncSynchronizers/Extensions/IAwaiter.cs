using System;

namespace Serie2.AsyncLike.Extensions
{
    public interface IAwaiter<out T>
    {
        bool BeginAwait(Action continuation);
        T EndAwait();
        bool TryCancelAwait(out Action continuation);
        IAwaiter<T> GetAwaiter();
    }


    public interface IAwaiter
    {
        bool BeginAwait(Action continuation);
        void EndAwait();
        bool TryCancelAwait(out Action continuation);
        IAwaiter GetAwaiter();
    }
}
