using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class DownloadFile
{

    UnityWebRequest www;
    string url;
    public delegate void Download(float progress);
    public event Download OnDownload;

    bool downloadInterrupted;
    byte[] bytes = new byte[2000];

    public DownloadFile(string url)
    {
        this.url = url;
        www = UnityWebRequest.Get(url);
        www.downloadHandler = new DownloadFileWebRequest(bytes);

        www.SendWebRequest();
    }

    public UnityWebRequest File
    {
        get
        {
            if (www == null)
            {
                throw new System.NullReferenceException();
            }
            return www;
        }
    }

    public IEnumerator IEDownload(UnityAction complete)
    {
        DownloadFileWebRequest w;
        w = (DownloadFileWebRequest)www.downloadHandler;
        while (!Caching.ready)
            yield return null;

        while (!www.isDone)
        {
            //Debug.Log(string.Format("dl {0:P1}", www.downloadProgress));
            yield return null;
        }
        if (complete != null) complete();

        complete = delegate { };

        //using (www = UnityWebRequest.Get(url))
        //{
        //    www.downloadHandler = new DownloadFileWebRequest(bytes);
        //    if (www.isDone) Debug.Log("dowload finished");
        //    else Debug.Log("downloading " + url);



        //    while (!www.isDone)
        //    {

        //        Debug.Log("downloading file " + www.downloadedBytes);

        //        if (OnDownload != null) { OnDownload(www.downloadProgress); }

        //        if (Application.internetReachability == NetworkReachability.NotReachable)
        //        {
        //            downloadInterrupted = true;
        //            break;
        //        }
        //        yield return new WaitForFixedUpdate();
        //    }

        //    if (Application.internetReachability != NetworkReachability.NotReachable && www.isDone)
        //    {
        //        downloadInterrupted = false;
        //    }

        //    if (www.error != null)
        //    {
        //        MessageBox.ins.ShowOk("error:9001" + www.error, MessageBox.MsgIcon.msgError, null);
        //    }
        //    else if (downloadInterrupted)
        //    {

        //        MessageBox.ins.ShowOk("error:9000\nINTERNET CONNECTION FAILED.\n", MessageBox.MsgIcon.msgError, null);
        //    }
        //    else
        //    {
        //        if (complete != null) complete();
        //        complete = delegate { };
        //    }
        //}
        yield return www;
    }



}
