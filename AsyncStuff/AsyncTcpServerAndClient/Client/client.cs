using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

class Client
{
    const int SERVER_PORT = 10000;
    static CancellationTokenSource cts =new CancellationTokenSource();
    // execute get file request	
    static async Task GetFile(string fname, NetworkStream stream)
    {

        // create the request message
        GetFileRequest request = new GetFileRequest(fname);
        // send the message to the server
        await MessageUtils.SendMsg(request, stream);
        // wait for and read the response
        GetFileResponse response = await MessageUtils.RecvMsg(stream) as GetFileResponse;
        if (response == null)
        {
            Console.WriteLine("+++ Client: wrong response from server: {0}", response);
            return;
        }
        if (response.IsOK)
        {
            try
            {
                var file = new FileStream(@"tmp\" + fname, FileMode.OpenOrCreate);
                await MessageUtils.ReceiveFile(stream, file, cts.Token);
                Console.WriteLine("+++ Client: file written to: {0}", @"tmp\" + fname);
            }
            catch (IOException ex)
            {
                Console.WriteLine("+++ Client: I/O exception: {0}", ex.Message);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("+++ Client: File Copy Cancelled");
            }
        }
        else
        {
            Console.WriteLine("+++ Client: receive file error: {0}", response.ResultMsg);
        }
    }

    // client entry point
    static void Main(string[] args) {
#if Release

        if (args.Length == 0) {
            Console.WriteLine("usage: client file1 file2 ...");
            return;
        }
#else

        args = new[] { Console.ReadLine() };
        
#endif

        clientRequest(args);
        Console.ReadLine();
        cts.Cancel();
        Console.WriteLine("cancelled");
        Console.ReadLine();
    }

    public async static void clientRequest(string[] args)
    {
        try
        {
            foreach (string fname in args)
            {
                Console.WriteLine("Getting server");
                TcpClient connection = new TcpClient("localhost", SERVER_PORT);


                await GetFile(fname, connection.GetStream());
                connection.Close();

            }
        }
        catch (SocketException ex)
        {
            Console.WriteLine("--- Client exception: {0}", ex);
        }
    }
}
