using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serie2.AsyncLike.Extensions;

namespace Serie2.AsyncLike.Tests
{
    public class AsyncExchangerTest
    {

        private const int NR_THREADS = 100;

        public async static void RunWithTimeout()
        {
            var exchanger = new AsyncExchanger<string>();
            var exchangerRetValues = new bool[NR_THREADS+1];
            var exchangerExceptionValues = new bool[NR_THREADS+1];
            var tasks = 0.To(NR_THREADS+1).Select(myId => TaskEx.Run<Task>(

            async () =>
            {
                try
                {
                    var exchangeId = await exchanger.Exchange(myId.ToString()).WithTimeout(1);
                    exchangerRetValues[Int32.Parse(exchangeId)] = true;
                }
                catch (TimeoutException exception)
                {
                    Console.WriteLine("Timeout");
                    exchangerExceptionValues[myId] = true;
                }

            }).Unwrap()).ToArray();

            Task.WaitAll(tasks);
            Console.WriteLine("Ended With Timeout");
            CheckWithException(exchangerRetValues, exchangerExceptionValues);

        }


        public static void Run()
        {
            var exchanger = new AsyncExchanger<string>();
            var exchangerRetValues = new bool[NR_THREADS];
            var tasks = 0.To(NR_THREADS).Select(myId => TaskEx.Run<Task>(
        
            async () =>
            {
              
                var exchangeId = await exchanger.Exchange(myId.ToString());
                exchangerRetValues[Int32.Parse(exchangeId)] = true;
               
            }).Unwrap()).ToArray();

            Task.WaitAll(tasks);
            Console.WriteLine("ENDED");
            Check(exchangerRetValues);
        }

       

        private static void Check(bool[] _exchangerRetValue)
        {
            for (int idx = 0; idx < NR_THREADS; idx++)
                if (!_exchangerRetValue[idx])
                { 
                    Console.WriteLine("Something broke");
                    return;
                }
            Console.WriteLine("All Done");
        }

        private static void CheckWithException(bool[] _exchangerRetValue, bool[] _exchangerExceptionValue)
        {
            for (int idx = 0; idx < NR_THREADS; idx++)
                if (!_exchangerRetValue[idx] && !_exchangerExceptionValue[idx])
                {
                    Console.WriteLine("Something broke");
                    return;
                }
            Console.WriteLine("All Done");
        }
    }

    public static class Extensions
    {
        public static IEnumerable<int> To(this int from, int to)
        {
            
            while(from < to)
            yield return from++;
        }


    }

    
}
