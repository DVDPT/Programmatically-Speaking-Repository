using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;



class Server
{
    // Port used by server
    const int SERVER_PORT = 10000;
    private const string SERVER_DIRECTORY = @"D:\Downloads\Torrent\Finished";

    // The server lsitener socket
    private static TcpListener _Listener = null;

    // The logger
    private static readonly Logger _Logger = new Logger();


    // ServerController
    private static readonly ServerController _Controller = new ServerController();
    // Process a request

    private static readonly Dictionary<Type, Func<object, NetworkStream, Task>> _requestsRoutines = new Dictionary<Type, Func<object, NetworkStream, Task>>()
                                                                                 {
                                                                                     {typeof(object)         , DefaultProcessRequest},
                                                                                     {typeof(GetFileRequest) , ProcessFileRequest },
                                                                                     {typeof(GetHttpRequest) , ProcessHttpRequest}
                                                                                 };

    static async Task ProcessRequest(object request, NetworkStream client_stream)
    {
        var type = request.GetType();
        Func<object, NetworkStream, Task> func;
        func = _requestsRoutines.ContainsKey(type) ? _requestsRoutines[type] : _requestsRoutines[typeof(object)];

        try
        {
            await func(request, client_stream);
        }
        catch (Exception e)
        {
            _Logger.Log(String.Format("Error while processing the request: {0}", e.Message));
        }


    }

    static async Task ProcessHttpRequest(object request, NetworkStream client_stream)
    {
        var req = request as GetHttpRequest;
        object response = null;
        var exception = false;
        _Logger.IncReq();
        try
        {
            _Logger.Log(String.Format("Getting : {0}", req.Uri));
            WebResponse webResponse = await HttpWebRequest.Create(req.Uri).GetResponseAsync();
           

            var memoryStream = new MemoryStream();
            var stream = webResponse.GetResponseStream();
            var buf = new byte[1024];
            var count = 0;
            while ((count = await stream.ReadAsync(buf, 0, 1024)) != 0)
                memoryStream.Write(buf, 0, count);

            await MessageUtils.SendMsg(new GetFileResponse(memoryStream.Length), client_stream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            await MessageUtils.SendFile(memoryStream, client_stream, new CancellationTokenSource());
        }
        catch (WebException e)
        {
            exception = true;
            response = new GetFileResponse(e.Message);
           
            
        }
        catch (UriFormatException)
        {
            exception = true;
            response = new GetFileResponse("the uri is not valid");
        }
        catch (TaskCanceledException ex)
        {
            _Logger.Log("Server: User Cancelled the uri copy");
        }
       
        if(exception)
            await MessageUtils.SendMsg(response, client_stream);
        
        
        
    }


    static async Task DefaultProcessRequest(object request, NetworkStream client_stream)
    {
        await MessageUtils.SendMsg("*** SERVER ERROR: unknown request message", client_stream);

    }

    static async Task ProcessFileRequest(object request, NetworkStream client_stream)
    {
        var gfr = request as GetFileRequest;
        object response = null;
        bool exception = false;

        _Logger.IncReq();
        var filePath = String.Format("{0}\\{1}", SERVER_DIRECTORY, gfr.FileName);
        _Logger.Log(String.Format("Server: get file \"{0}\"", filePath));
        try
        {
            var file = new FileStream(filePath, FileMode.Open, FileSystemRights.Read, FileShare.Read, 4096 * 2, FileOptions.Asynchronous);
            //byte[] fbytes = File.ReadAllBytes(filePath);
            response = new GetFileResponse(file.Length);

            //Send ok message do user
            await MessageUtils.SendMsg(response, client_stream);
            await MessageUtils.SendFile(file, client_stream, new CancellationTokenSource());
            _Logger.Log(String.Format("Server: return file \"{0}\" OK", gfr.FileName));
        }
        catch (IOException ex)
        {
            exception = true;
            response = new GetFileResponse(ex.Message);
            _Logger.Log(String.Format("Server: error on get file \"{0}\": {1}",
                                     gfr.FileName, ex.Message));

        }
        catch (TaskCanceledException ex)
        {
            _Logger.Log("Server: User Cancelled the file copy");
        }
        if (exception)
            await MessageUtils.SendMsg(response, client_stream);


    }

    // Handler for CTRL-C
    // -- Closes the server listener, stops the logger and exits


    // server main method
    static void Main(string[] args)
    {
        ServerStartAsync();
        Console.Read();
        _Logger.Log("Server shutting down");
        _Listener.Stop();
        _Controller.ShoutDown();
        _Logger.Stop();
        Console.ReadLine();

    }

    static void ServerStartAsync()
    {

        _Logger.Start();
        try
        {
            _Listener = new TcpListener(IPAddress.Any, SERVER_PORT);
            _Listener.Start();
            _Logger.Log("server: ready to accept requests");
            _Logger.Log(String.Format("server: running on {0}", SERVER_DIRECTORY));
            AcceptClientsAsync(_Logger);

        }
        catch (SocketException ex)
        {
            _Logger.Log(String.Format("Server exception: {0}", ex));
        }



    }
    static async void AcceptClientsAsync(Logger logger)
    {
        bool entered = false;
        TcpClient client = null;
        try
        {

            client = await _Listener.AcceptTcpClientAsync();
            entered = true;
            _Controller.ClientEntered();
            AcceptClientsAsync(logger);
            logger.Log("Client " + client.Client.RemoteEndPoint);
            NetworkStream client_stream = client.GetStream();

            // Get the request message 
            object request = await MessageUtils.RecvMsg(client_stream);

            // Process the request message and send the response
            await ProcessRequest(request, client_stream);


        }
        catch (SocketException ex)
        {
            logger.Log(String.Format("Server exception: {0}", ex));
        }
        catch (ObjectDisposedException) { }

        catch (Exception ex)
        {
            logger.Log(String.Format("Server exception: {0}", ex));
        }
        finally
        {
            if (entered)
            {
                if (client != null)
                    try
                    {
                        (client as IDisposable).Dispose();
                    }
                    catch (ObjectDisposedException) { }

                _Controller.ClientExiting();
            }
        }

    }

    public class ServerController
    {
        private volatile int _currClients;
        private volatile bool _isShuttingDown;

        public void ClientEntered()
        {
            Interlocked.Increment(ref _currClients);
        }

        public void ClientExiting()
        {
            if (Interlocked.Decrement(ref _currClients) == 0 && _isShuttingDown)
            {
                lock (this)
                    Monitor.Pulse(this);
            }

        }

        public void ShoutDown()
        {
            _isShuttingDown = true;
            if (_currClients == 0)
                return;

            lock (this)
                while (_currClients != 0)
                    Monitor.Wait(this);
        }
    }
}
