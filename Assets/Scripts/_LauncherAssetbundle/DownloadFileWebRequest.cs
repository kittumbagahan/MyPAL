using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class DownloadFileWebRequest : DownloadHandlerScript {

    public int contentLen;
    public byte[] bitsy = new byte[2000];
    // Standard scripted download handler - will allocate memory on each ReceiveData callback
    public DownloadFileWebRequest()
        : base()
    {
    }

    // Pre-allocated scripted download handler
    // Will reuse the supplied byte array to deliver data.
    // Eliminates memory allocation.
    public DownloadFileWebRequest(byte[] buffer)
        : base(buffer)
    {
    }

    // Required by DownloadHandler base class. Called when you address the 'bytes' property.
    protected override byte[] GetData() { return null; }

    protected override string GetText()
    {
        return base.GetText();
    }

    // Called once per frame when data has been received from the network.
    protected override bool ReceiveData(byte[] byteFromServer, int dataLength)
    {
        
        if (byteFromServer == null || byteFromServer.Length < 1)
        {
            Debug.Log("CustomWebRequest :: ReceiveData - received a null/empty buffer");
            return false;
        }
        bitsy = byteFromServer;
        Debug.Log(string.Format("byte{0} datalength{1}", byteFromServer[0].ToString(), dataLength));
        //Do something with or Process byteFromServer here


        return true;
    }

    // Called when all data has been received from the server and delivered via ReceiveData
    protected override void CompleteContent()
    {
       
        Debug.Log("CustomWebRequest :: CompleteContent - DOWNLOAD COMPLETE!");
    }

    // Called when a Content-Length header is received from the server.
    protected override void ReceiveContentLength(int contentLength)
    {
        contentLen = contentLength;
        Debug.Log(string.Format("CustomWebRequest :: ReceiveContentLength - length {0}", contentLength));
    }
}
