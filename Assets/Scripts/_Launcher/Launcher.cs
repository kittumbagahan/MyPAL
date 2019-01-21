using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using BeardedManStudios;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;

public sealed class Launcher : CachedAssetBundleLoader
{
   [SerializeField]
   Button btnUpdate;
   [SerializeField]
   Button btnStart;
   [SerializeField]
   Button btnCancel;
   [SerializeField]
   UnityEngine.UI.Text txtAppVersion;
   [SerializeField]
   GameObject pnlLoading;

   [SerializeField]
   LauncherNetworking lNet;
   [SerializeField]
   ProgressBar pb;

   int findingServerTime = 0;
   private int retryCount;


   [SerializeField]
   string testUrl; //include extension name
   [SerializeField]
   int testVersion;

   int numOfFilesToDownload = 0;

   void Start()
   {
      lNet.Initialize ();
      lNet.OnFindingServer += FindingServer;
      lNet.OnAssetBundleDataReceived += DownloadAssetBundle;

      txtAppVersion.text = "version " + PlayerPrefs.GetInt ("productVersion") + "." + PlayerPrefs.GetInt ("releaseVersion") + "." + PlayerPrefs.GetInt ("bundleVersion");
      btnCancel.gameObject.SetActive (false);
      pnlLoading.SetActive (false);

   }

   public void CheckForUpdate()
   {
      if (Application.internetReachability == NetworkReachability.NotReachable)
      {
         MessageBox.ins.ShowOk ("No connection.", MessageBox.MsgIcon.msgError, null);
      }
      else
      {
         pb.gameObject.SetActive (true);
         btnUpdate.interactable = false;
         btnStart.interactable = false;
         btnCancel.gameObject.SetActive (true);
         lNet.stopSearch = false;
         findingServerTime = 0;
         //check for open network
         //if there is open network connect
         lNet.FindServer ();
         OnDownload += pb.SetProgress;

         bundleURL = testUrl;
         bundleVersion = testVersion;
         //Caching.ClearCache();
         //PlayerPrefs.SetInt("bundleVersion", 0);


      }
   }

   public void CancelCheckUpdate()
   {
      //cancel will be disabled once the client succesfully connected to the server
      Debug.Log ("Check for update cancelled.");
      btnCancel.gameObject.SetActive (false);
      btnUpdate.interactable = true;
      btnStart.interactable = true;
      pb.gameObject.SetActive (false);
      lNet.stopSearch = true;
      //StopCoroutine(lNet.coFind);

   }

   private void DownloadAssetBundle(AssetBundleDataCollection assetBundleDataCollection)
   {
      Debug.Log ("I am connected");
      numOfFilesToDownload = assetBundleDataCollection.lstAssetBundleData.Count;

      MainThreadManager.Run (() =>
       {
          pb.TextTitle.text = "Connection success!";
          //wait for the server to send download url
          //automatically accept
          //check bundle version
          if (CheckBundleCollectionBatchNumber (assetBundleDataCollection.batchN))
          {
             MessageBox.ins.ShowOk ("Error: Collection outdated!", MessageBox.MsgIcon.msgError, null);

             return;
          }

          StartCoroutine (IEDownloadPool(assetBundleDataCollection));
          

         

          //if (CheckBundleVersion (assetBundleDataCollection.lstAssetBundleData[0].version))
          //{

          //}
          //else
          //{
          //   pb.SetProgress (1);
          //   MessageBox.ins.ShowOk ("Version is up to date.", MessageBox.MsgIcon.msgInformation,
          //         () =>
          //         {
          //            StartGame ();
          //         });
          //}

       });


   }

   public void StartGame()
   {

      //PlayerPrefs.SetString ("bundleUrl", "https://www.dropbox.com/s/9inhox6owsspn40/assetbundlebookshelf.1?dl=1");
      //PlayerPrefs.SetInt ("bundleVersion", 1);
      //PlayerPrefs.SetString ("bundleUrl", "");
      //PlayerPrefs.SetInt ("bundleVersion", 0);

      if (PlayerPrefs.GetString ("BookShelf_url_key") == "")
      {
         SceneManager.LoadSceneAsync ("BookShelf");
      }
      else
      {
         LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle (PlayerPrefs.GetString ("BookShelf_url_key"), PlayerPrefs.GetInt ("BookShelf_version_key"));
         loader.OnLoadSceneFail += FailLoadBookShelf;
         loader.OnLoadSceneSuccess += SuccessLoadBookShelf;

         StartCoroutine (loader.IEStreamAssetBundle ());
      }
   


   }

   void FailLoadBookShelf()
   {
      pnlLoading.SetActive (true);
      Debug.Log ("bookshelf loaded default");
      SceneManager.LoadSceneAsync ("BookShelf");
   }
   void SuccessLoadBookShelf()
   {
      Debug.Log ("bookshelf loaded assetbundle");
      pnlLoading.SetActive (true);

   }

   IEnumerator IEDownloadPool(AssetBundleDataCollection assetBundleDataCollection)
   {
      for (int i = 0; i < assetBundleDataCollection.lstAssetBundleData.Count; i++)
      {
         AssetBundleData asd = assetBundleDataCollection.lstAssetBundleData[i];
         //download assetbundle from url
         yield return StartCoroutine (IEDownload (asd));
         //on download completed load the bundle

         Debug.Log ("downloading " + assetBundleDataCollection.lstAssetBundleData[i].url);
      }
   }

   IEnumerator IEDownload(AssetBundleData assetBundleData)
   {
      //*NOTE when we have downloaded versions bundle you can switch to previous version by setting the bundleVersion without changing bundleURL
      //*always download assetbundle together with its url and version number
      if(assetBundleData.assetCategory != AssetBundleCategory.SECTION_BOOK_DB_DATA_FILE || assetBundleData.assetCategory != AssetBundleCategory.STORYBOOK_ACTIVITY_SECLECTION_DATA_FILE)
      {
         yield return StartCoroutine (IEGetFromCacheOrDownload (assetBundleData.url, assetBundleData.version));
      }
      else
      {
         DownloadFile df = new DownloadFile (assetBundleData.url);
         df.OnDownload += pb.SetProgress; //need cachedassetbunldeloader reference to check for any error during download
         yield return StartCoroutine (df.IEDownload());

         switch (assetBundleData.assetCategory)
         {
            case AssetBundleCategory.SECTION_BOOK_DB_DATA_FILE:
               SaveBookAndActivityData.SaveToDatabase (df.File);
               //save to database
               break;
            case AssetBundleCategory.STORYBOOK_ACTIVITY_SECLECTION_DATA_FILE:
               //append to playerprefs
               break;
            default: break;
         }
      }

      switch (assetBundleData.assetCategory)
      {
         case AssetBundleCategory.BOOKSHELF_SCENE:
            PlayerPrefs.SetString ("BookShelf_url_key", assetBundleData.url);
            PlayerPrefs.SetInt ("BookShelf_version_key", assetBundleData.version);
            break;
         case AssetBundleCategory.ACTIVITY_SELECTION_SCENE:
            PlayerPrefs.SetString ("ActivitySelection_url_key", assetBundleData.url);
            PlayerPrefs.SetInt ("ActivitySelection_version_key", assetBundleData.version);
            break;
         case AssetBundleCategory.BOOK_SCENE:
            PlayerPrefs.SetString (assetBundleData.description + "_url_key", assetBundleData.url);
            PlayerPrefs.SetInt (assetBundleData.description + "_version_key", assetBundleData.version);
            break;
         case AssetBundleCategory.ACTIVITY_SCENE:
            PlayerPrefs.SetString (assetBundleData.description + "_url_key", assetBundleData.url);
            PlayerPrefs.SetInt (assetBundleData.description + "_version_key", assetBundleData.version);
            break;
         case AssetBundleCategory.LAUNCHER_SCENE:
            PlayerPrefs.SetString ("Launcher_url_key", assetBundleData.url);
            PlayerPrefs.SetInt ("Launcher_version_key", assetBundleData.version);
            break;
         case AssetBundleCategory.SECTION_BOOK_DB_DATA_FILE:
            //save to database
            break;
         case AssetBundleCategory.STORYBOOK_ACTIVITY_SECLECTION_DATA_FILE:
            //append to playerprefs
            break;
         default: break;
      }

      
      if (success)
      {
         //set progress to next download
         pb.TextTitle.text = string.Format("Downloading {0}/{1}", downloadCnt, numOfFilesToDownload);
         pb.SetProgress (0);
         success = false;
      }
      else
      {
         if (retryCount < 20)
         {
            pb.TextTitle.text = "Connection error... Retrying download...";
            yield return new WaitForSeconds (1f);
            //stop exisiting download coroutine here
            retryCount++;
            StartCoroutine (IEDownload (assetBundleData));
         }
         else
         {
            MessageBox.ins.ShowOk ("error:9000\nINTERNET CONNECTION FAILED.", MessageBox.MsgIcon.msgError, () => { ConnectionErrMsgRetry (assetBundleData); });
         }
      }

      if (downloadCnt >= numOfFilesToDownload)
      {
         PlayerPrefs.SetInt ("bundleVersion", assetBundleData.version); // reference importanteu for loading the bundle from cache this serves as its key
         PlayerPrefs.SetString ("bundleUrl", assetBundleData.url); // reference importanteu for loading the bundle from cache this serves as its key
         pb.TextTitle.text = "Download Complete";
         OnDownload -= pb.SetProgress;
         print ("Complete download");
         if (bundle.isStreamedSceneAssetBundle)
         {
            string[] scenePaths = bundle.GetAllScenePaths ();
            string sceneName = System.IO.Path.GetFileNameWithoutExtension (scenePaths[0]);
            pnlLoading.SetActive (true);
            SceneManager.LoadSceneAsync (sceneName);
         }
      }

   }


   private void FindingServer()
   {
      //Debug.Log(string.Format("finding server counting={0}", findingServerTime));
      pb.TextTitle.text = "Finding server " + findingServerTime.ToString () + "s";
      findingServerTime += 1;
      if (findingServerTime >= 60)
      {
         Debug.Log ("What to do?");
      }
      if (Application.internetReachability == NetworkReachability.NotReachable)
      {
         MessageBox.ins.ShowOk ("No connection. Do something here.", MessageBox.MsgIcon.msgError, null);
         //stop lNet.FindServer();
      }
   }

   bool CheckBundleVersion(int serverBundleVersion)
   {
      if (serverBundleVersion > PlayerPrefs.GetInt ("bundleVersion"))
      {
         return true;
      }
      return false;
   }

   bool CheckBundleCollectionBatchNumber(int n)
   {
      if (n > PlayerPrefs.GetInt ("bundleCollectionBatchNumber"))
      {
         return true;
      }
      return false;
   }


   #region Messages

   void ConnectionErrMsgRetry(AssetBundleData assetBundleData)
   {
      MessageBox.ins.ShowQuestion ("Retry download?", MessageBox.MsgIcon.msgInformation, () => { RetryDownload (assetBundleData); }, new UnityAction (BeforeCloseMsg));
   }

   void RetryDownload(AssetBundleData assetBundleData)
   {
      retryCount = 0;
      StartCoroutine (IEDownload (assetBundleData));
   }

   void BeforeCloseMsg()
   {
      MessageBox.ins.ShowOk ("Try downloading your books later.", MessageBox.MsgIcon.msgInformation, null);
   }
   #endregion
}
