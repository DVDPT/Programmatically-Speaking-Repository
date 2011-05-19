using System;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Messages;
using Serie2.AsyncLike.Extensions;

//
// Utility methods to create, send, and receive messages using a NetworkStream
//
// A message has two fields:
//  - length (4 bytes): message size, including 'length' itself
//  - payload (length - 4 bytes): a byte stream obtained through serialization of an object.

public class MessageUtils
{
    public const int MSG_HDR_SIZE = sizeof(int);


    public static async Task SendMsg(object message, NetworkStream stream)
    {
        MemoryStream memstm = new MemoryStream();
        BinaryFormatter fmtr = new BinaryFormatter();
        memstm.Seek(MSG_HDR_SIZE, SeekOrigin.Begin);
        // serialize object graph to stream
        fmtr.Serialize(memstm, message);
        // get the buffer from the memory stream
        byte[] membuf = memstm.ToArray();
        // get the byte array with the message size
        byte[] lenbuf = BitConverter.GetBytes(membuf.Length);
        // copy the length to the first four bytes of the message
        for (int i = 0; i < lenbuf.Length; i++)
            membuf[i] = lenbuf[i];
        // send the message synchronously
        await stream.WriteAsync(membuf, 0, membuf.Length);

    }

    // Receive a message synchronously
    public static async Task<object> RecvMsg(NetworkStream stream)
    {
        byte[] lenbuf = new byte[MSG_HDR_SIZE];
        // receive message length
        int bytesRead = await stream.ReadAsync(lenbuf, 0, lenbuf.Length);
        if (bytesRead != MSG_HDR_SIZE)
            throw new ApplicationException(String.Format("RecvMessage: unexpected message length size with {0} bytes", bytesRead));
        // receive message payload
        return await RecvMsg(stream, lenbuf);
    }

    // Receive message payload synchronously, after length has been received
    private static async Task<object> RecvMsg(NetworkStream stream, byte[] lenbuf)
    {
        // compute message length
        int memlen = BitConverter.ToInt32(lenbuf, 0) - MSG_HDR_SIZE;
        // allocate a byte array for the remainder of message
        byte[] membuf = new byte[memlen];
        // receive the remainder of message
        int bytesRead = 0;
        while (bytesRead < memlen)
        {
            bytesRead += await stream.ReadAsync(membuf, bytesRead, memlen - bytesRead);
        }
        // create a stream from message content and get the object graph
        MemoryStream memstm = new MemoryStream(membuf);
        BinaryFormatter fmtr = new BinaryFormatter();
        return fmtr.Deserialize(memstm);
    }


    public static async Task SendFile(Stream src, NetworkStream dst, CancellationTokenSource cts)
    {
        await Utils.StreamCopy(src, dst, cts).Wait().WrapToGenericAwaiter().WithCancellation(cts.Token);
    }

    public static async Task ReceiveFile(NetworkStream src, Stream dst, CancellationToken token, Action<int> onProgress = null)
    {
        await Utils.StreamCopy(src, dst, token, onProgress).Wait().WrapToGenericAwaiter().WithCancellation(token);
    }

    
}

//
// Messages used to communicate between the clients and the servr
//

// message used for get file request

public interface IRequest
{
    string Request { get; }
}

public interface IResponse
{
    long ResponseSize { get; }
    bool IsOK { get; }
    string ResultMsg { get; }
}

[Serializable]
public class GetFileRequest : IRequest
{
    private string fname;
    public GetFileRequest(string fname) { this.fname = fname; }
    public string FileName { get { return fname; } }

    public string Request
    {
        get { return FileName; }
    }
}

// message used for get file response
[Serializable]
public class GetFileResponse : IResponse
{
    private bool ok;			// true if ok, false if error
    private string errmsg;
    private long _filesize;
    public GetFileResponse(string errmsg)
    {
        this.ok = false;
        this.errmsg = errmsg;
    }

    public GetFileResponse(long filesize)
    {
        this.ok = true;
        this.errmsg = null;
        _filesize = filesize;
    }

    public bool IsOK { get { return ok; } }

    public string ResultMsg
    {
        get
        {
            if (ok)
                return "Response OK";
            return errmsg;
        }

    }

    public long FileSize { get { return _filesize; } }


    public long ResponseSize
    {
        get { return FileSize; }
    }
}

[Serializable]
public class GetHttpRequest : IRequest
{
    public string Uri { get; private set; }

    public GetHttpRequest(string uri)
    {
        Uri = uri;
    }

    public string Request
    {
        get { return Uri; }
    }
}
