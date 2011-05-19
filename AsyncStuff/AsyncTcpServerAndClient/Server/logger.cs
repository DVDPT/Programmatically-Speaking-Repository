using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;


// Single-threaded Logger
public class Logger
{
   
    private int _reqCount = 0;	// number of requests
    private DateTime _startTime;
    private readonly Task _loggerTask;
    private readonly TextWriter _lwriter;
    private readonly CancellationTokenSource _cts = new CancellationTokenSource();
    private readonly BufferBlock<string> _buf = new BufferBlock<string>();

    public Logger() : this(Console.Out) { }

    public Logger(TextWriter writer)
    {
        _lwriter = writer;
        _loggerTask = Task.Factory.StartNew(() =>
                {
                    var cToken = _cts.Token;
                    for (; ; )
                    {
                        _lwriter.WriteLine(_buf.Receive());
                        if (cToken.IsCancellationRequested && _buf.Count == 0)
                            return;
                
                    }
                
                });

    }

    public void Start() { _startTime = DateTime.Now; }

    public void Stop()
    {
        long elapsed = DateTime.Now.Ticks - _startTime.Ticks;
        Log(String.Format("server running for {0} seconds", elapsed / 10000000L));
        Log(String.Format("server processed {0} requests", _reqCount));
        _cts.Cancel();
        Task.WaitAll(_loggerTask);
    }

    public async void Log(string msg)
    {
        
        await _buf.PostAsync(String.Format("{0}: {1}", DateTime.Now, msg));
    }



    public void IncReq() { Interlocked.Increment(ref _reqCount); }
}
