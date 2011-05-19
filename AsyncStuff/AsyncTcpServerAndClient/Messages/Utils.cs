using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Serie2.AsyncLike;
using Serie2.AsyncLike.Extensions;

namespace Messages
{
    class Utils
    {
        private static readonly byte[] _CancelationRequest = new byte[] {0x1,0x2};


        private const int BLOCK_SIZE = 4096 * 10;


        public static AsyncEvent StreamCopy(NetworkStream src, Stream dst, CancellationToken cts,Action<int> onProgress = null)
        {
            Func<bool> cancelFunc = () => !cts.IsCancellationRequested;
            cts.Register(() => src.Write(_CancelationRequest, 0, _CancelationRequest.Length));
            return StreamCopy(src, dst, cancelFunc, null,onProgress);
        }
        public static AsyncEvent StreamCopy(Stream src, NetworkStream dst, CancellationTokenSource cts)
        {
            Func<bool> checkForCancellation = () =>
                                           {

                                               if (dst.DataAvailable)
                                               {
                                                   src.Close();
                                                   cts.Cancel();
                                                   return false;
                                               }
                                               return true;
                                           };


            return StreamCopy(src, dst, checkForCancellation, 
                ()=> 
                { src.Close(); cts.Cancel();});


        }


        
        private static AsyncEvent StreamCopy(Stream src, Stream dst, Func<bool> assertContinuation, Action onException = null, Action<int> onProgress = null)
        {
            var evt = new AsyncEvent(false, false);
            var exception = false;
            AsyncCallback cb = null;
            cb = (ar) =>
            {
                var count = src.EndRead(ar);
                var readed = (byte[])ar.AsyncState;
                if (exception) return;

                if (count != 0)
                {
                    lock (evt)
                    {
                        if (exception) return;
                        var newArray = new byte[BLOCK_SIZE];
                        if (!assertContinuation())
                         return;

                        if (onProgress != null)
                            onProgress(count);

                        src.BeginRead(newArray, 0, BLOCK_SIZE, cb, newArray);
                        try
                        {
                            dst.Write(readed, 0, count);
                        }
                        catch (Exception e)
                        {
                            exception = true;
                            if(onException != null)
                                onException();
                            
                        }
                    }
                }
                else
                {
                    lock (evt)
                    {
                        src.Close();
                        evt.Set();
                    }
                }
            };
            var startBuffer = new byte[BLOCK_SIZE];
            src.BeginRead(startBuffer, 0, BLOCK_SIZE, cb, startBuffer);
            return evt;

        }



    }
}
