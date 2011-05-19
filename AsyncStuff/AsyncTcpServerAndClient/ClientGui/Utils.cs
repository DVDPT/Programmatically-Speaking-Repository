using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ClientGui
{


    public class Utils
    {
        public static string RelativePath = @"tmp\";

        public static async Task<bool> GetFile(IRequest request, string fname, NetworkStream stream, Client.FileRepresentative representative, TaskScheduler scheduler)
        {

            // send the message to the server
            await MessageUtils.SendMsg(request, stream);
            // wait for and read the response
            IResponse response = await MessageUtils.RecvMsg(stream) as GetFileResponse;

            if (response == null)
                return false;

            if (response.IsOK)
            {
                var cts = new CancellationTokenSource();

                EventHandler cancelEvent = (_, __) => cts.Cancel();
                representative._cancel.Click += cancelEvent;
                FileStream file = null;
                try
                {
                    file = new FileStream(RelativePath + fname, FileMode.OpenOrCreate);
                    var initialCount = 0;



                    Action<int> onProgress = async (count) =>
                    {

                        await scheduler.SwitchTo();
                        long newCount = Interlocked.Add(ref initialCount, count);
                        representative.UpdateBar((int)(((float)newCount / (float)response.ResponseSize) * 100));
                    };

                    await MessageUtils.ReceiveFile(stream, file, cts.Token, onProgress);
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    representative._cancel.Click -= cancelEvent;
                    if (file != null)
                        file.Close();
                }


            }


            return false;


        }
    }
}
