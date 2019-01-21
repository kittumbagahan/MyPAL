using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DownloadFile
{

   WWW www;
   string url;
   public delegate void Download(float progress);
   public event Download OnDownload;
   public DownloadFile(string url)
   {
      this.url = url;
   }

   public WWW File
   {
      get {
         if(www == null)
         {
            throw new System.NullReferenceException();
         }
         return www;
      }
   }

   public IEnumerator IEDownload()
   {
      www = new WWW (url);
      if(OnDownload != null)
      {
         OnDownload(www.progress);
      }

      OnDownload = delegate { };
      yield return www;
   }



}
