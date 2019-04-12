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
using _Assetbundle;

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
        lNet.Initialize();
        lNet.OnFindingServer += FindingServer;
        lNet.OnAssetBundleDataReceived += DownloadAssetBundle;

        txtAppVersion.text = "version " + PlayerPrefs.GetInt("productVersion") + "." + PlayerPrefs.GetInt("releaseVersion") + "." + PlayerPrefs.GetInt("bundleVersion");
        btnCancel.gameObject.SetActive(false);
        pnlLoading.SetActive(false);

        //AssetBundleDataCollection abd = new AssetBundleDataCollection();

        //abd.lstAssetBundleData.Add(new AssetBundleData("https://www.dropbox.com/s/jnxawtihot7sstz/assetbundlebookshelf?dl=1", 0, AssetBundleCategory.BOOKSHELF_SCENE, 1, ""));
        //abd.lstAssetBundleData.Add(new AssetBundleData("https://www.dropbox.com/s/mjh4krttrm6ftho/book_test_1_scene?dl=1", 0, AssetBundleCategory.BOOK_SCENE, 1, "book_test_1"));
        //abd.lstAssetBundleData.Add(new AssetBundleData("https://www.dropbox.com/s/id5t2dwswng785v/book_test_1_act1_activity?dl=1", 0, AssetBundleCategory.ACTIVITY_SCENE, 1, "book_test_1_Act1"));
        //abd.lstAssetBundleData.Add(new AssetBundleData("https://www.dropbox.com/s/oy60ggf4had2hb0/launcher_scene?dl=1", 0, AssetBundleCategory.LAUNCHER_SCENE, 1, ""));
        //abd.lstAssetBundleData.Add(new AssetBundleData("https://www.dropbox.com/s/0dmjyi104idqyef/activity_selection_scene?dl=1", 0, AssetBundleCategory.ACTIVITY_SELECTION_SCENE, 1, ""));
        //abd.lstAssetBundleData.Add(new AssetBundleData("https://www.dropbox.com/s/231if2qc2s0tdtz/book_test_1_dataset.txt?dl=1", 0, AssetBundleCategory.SECTION_BOOK_DB_DATA_FILE, 1, ""));
        //abd.lstAssetBundleData.Add(new AssetBundleData("https://www.dropbox.com/s/gbbvordfg6hc3yf/storybook_activities_data.txt?dl=1", 0, AssetBundleCategory.STORYBOOK_ACTIVITY_SECLECTION_DATA_FILE, 1, ""));
        //OnDownload += pb.SetProgress;
        //DownloadAssetBundle(abd);
    }

    public void CheckForUpdate()// used by button
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            MessageBox.ins.ShowOk("No connection.", MessageBox.MsgIcon.msgError, null);
        }
        else
        {
            pb.gameObject.SetActive(true);
            btnUpdate.interactable = false;
            btnStart.interactable = false;
            btnCancel.gameObject.SetActive(true);
            lNet.stopSearch = false;
            findingServerTime = 0;
            //check for open network
            //if there is open network connect
            lNet.FindServer();
            OnDownload += pb.SetProgress;

        }
    }

    public void CancelCheckUpdate()
    {
        //cancel will be disabled once the client succesfully connected to the server
        Debug.Log("Check for update cancelled.");
        btnCancel.gameObject.SetActive(false);
        btnUpdate.interactable = true;
        btnStart.interactable = true;
        pb.gameObject.SetActive(false);
        lNet.stopSearch = true;
        //StopCoroutine(lNet.coFind);

    }

    private void DownloadAssetBundle(AssetBundleDataCollection assetBundleDataCollection)
    {
        Debug.Log("I am connected");
        numOfFilesToDownload = assetBundleDataCollection.lstAssetBundleData.Count;

        MainThreadManager.Run(() =>
        {
            pb.TextTitle.text = "Connection success!";
           //wait for the server to send download url
           //automatically accept
           //check bundle version
           if (CheckBundleCollectionBatchNumber(assetBundleDataCollection.batchN))
            {
                MessageBox.ins.ShowOk("Error: Collection outdated!", MessageBox.MsgIcon.msgError, null);

                return;
            }

            StartCoroutine(IEDownloadPool(assetBundleDataCollection));

        });


    }

    public void StartGame()
    {
        if (PlayerPrefs.GetString("BookShelf_url_key") == "")
        {
            SceneManager.LoadSceneAsync("BookShelf");
        }
        else
        {
            string url = PlayerPrefs.GetString("BookShelf_url_key");
            int version = PlayerPrefs.GetInt("BookShelf_version_key");

            EmptySceneLoader.ins.loadUrl = url;
            EmptySceneLoader.ins.loadVersion = version;
            EmptySceneLoader.ins.sceneToLoad = "BookShelf";
            EmptySceneLoader.ins.isAssetBundle = true;


            //this is from launcher so
            EmptySceneLoader.ins.unloadUrl = PlayerPrefs.GetString("Launcher_url_key");
            EmptySceneLoader.ins.unloadVersion = PlayerPrefs.GetInt("Launcher_version_key");
            EmptySceneLoader.ins.unloadAll = false;

            SceneManager.LoadSceneAsync("empty");
        }
    }

    void FailLoadBookShelf()
    {
        pnlLoading.SetActive(true);
        Debug.Log("bookshelf loaded default");
        SceneManager.LoadSceneAsync("BookShelf");
    }
    void SuccessLoadBookShelf()
    {
        Debug.Log("bookshelf loaded assetbundle");
        pnlLoading.SetActive(true);

    }

    IEnumerator IEDownloadPool(AssetBundleDataCollection assetBundleDataCollection)
    {
        for (int i = 0; i < assetBundleDataCollection.lstAssetBundleData.Count; i++)
        {
            AssetBundleData asd = assetBundleDataCollection.lstAssetBundleData[i];
            Debug.Log("downloading " + assetBundleDataCollection.lstAssetBundleData[i].assetCategory.ToString());
            //download assetbundle from url
            yield return StartCoroutine(IEDownload(asd));
            //on download completed load the bundle


        }
    }

    IEnumerator IEDownload(AssetBundleData assetBundleData)
    {
        pb.TextTitle.text = string.Format("Downloading " + assetBundleData.assetCategory.ToString() + " {0}/{1}", downloadCnt, numOfFilesToDownload);
        //*NOTE when we have downloaded versions bundle you can switch to previous version by setting the bundleVersion without changing bundleURL
        //*always download assetbundle together with its url and version number
        if (assetBundleData.assetCategory != AssetBundleCategory.SECTION_BOOK_DB_DATA_FILE && 
            assetBundleData.assetCategory != AssetBundleCategory.STORYBOOK_ACTIVITY_SECLECTION_DATA_FILE)
        {
            yield return StartCoroutine(IEGetFromCacheOrDownload(assetBundleData.url, assetBundleData.version));
        }
        else
        {
            //Debug.Log("ERROR IS HERE");
            DownloadFile df = new DownloadFile(assetBundleData.url);
            df.OnDownload -= pb.SetProgress;
            df.OnDownload += pb.SetProgress; //need cachedassetbunldeloader reference to check for any error during download
            yield return StartCoroutine(df.IEDownload(() => { success = true; downloadCnt++; }));
            if (df.File.isDone)
            {
                //success = true;
                switch (assetBundleData.assetCategory)
                {
                    case AssetBundleCategory.SECTION_BOOK_DB_DATA_FILE:
                        SaveBookAndActivityData.SaveToDatabase(df.File);
                        //save to database
                        break;
                    case AssetBundleCategory.STORYBOOK_ACTIVITY_SECLECTION_DATA_FILE:
                        SaveBookAndActivityData.SaveActivityToPlayerPrefs(df.File);

                        break;
                    default: break;
                }
            }

        }

       


        if (success)
        {
            switch (assetBundleData.assetCategory)
            {
                case AssetBundleCategory.BOOKSHELF_SCENE:
                    PlayerPrefs.SetString("BookShelf_url_key", assetBundleData.url);
                    PlayerPrefs.SetInt("BookShelf_version_key", assetBundleData.version);
                    Debug.Log("BOOKSHELF DOWNLOAD COMPLETE!");
                    break;
                case AssetBundleCategory.ACTIVITY_SELECTION_SCENE:
                    PlayerPrefs.SetString("ActivitySelection_url_key", assetBundleData.url);
                    PlayerPrefs.SetInt("ActivitySelection_version_key", assetBundleData.version);
                    break;
                case AssetBundleCategory.BOOK_SCENE:
                    PlayerPrefs.SetString(assetBundleData.description + "_url_key", assetBundleData.url);
                    PlayerPrefs.SetInt(assetBundleData.description + "_version_key", assetBundleData.version);
                    break;
                case AssetBundleCategory.ACTIVITY_SCENE:
                    PlayerPrefs.SetString(assetBundleData.description + "_url_key", assetBundleData.url);
                    PlayerPrefs.SetInt(assetBundleData.description + "_version_key", assetBundleData.version);
                    break;
                case AssetBundleCategory.LAUNCHER_SCENE:
                    PlayerPrefs.SetString("Launcher_url_key", assetBundleData.url);
                    PlayerPrefs.SetInt("Launcher_version_key", assetBundleData.version);
                    break;
                case AssetBundleCategory.SECTION_BOOK_DB_DATA_FILE:
                    //save to database
                    break;
                case AssetBundleCategory.STORYBOOK_ACTIVITY_SECLECTION_DATA_FILE:
                    //append to playerprefs
                    break;
                default: break;
            }
            //set progress to next download
            pb.TextTitle.text = string.Format("Downloading {0}/{1}", downloadCnt, numOfFilesToDownload);
            pb.SetProgress(0);
            success = false;
        }
        else
        {
            if (retryCount < 20)
            {
                pb.TextTitle.text = "Connection error... Retrying download...";
                yield return new WaitForSeconds(1f);
                //stop exisiting download coroutine here
                retryCount++;
                StartCoroutine(IEDownload(assetBundleData));
            }
            else
            {
                MessageBox.ins.ShowOk("error:9000\nINTERNET CONNECTION FAILED.", MessageBox.MsgIcon.msgError, () => { ConnectionErrMsgRetry(assetBundleData); });
            }
        }

        if (downloadCnt >= numOfFilesToDownload)
        {

            pb.TextTitle.text = "Download Complete";
            OnDownload -= pb.SetProgress;
            print("Complete download");

            string url = PlayerPrefs.GetString("BookShelf_url_key");
            int version = PlayerPrefs.GetInt("BookShelf_version_key");

            EmptySceneLoader.ins.loadUrl = url;
            EmptySceneLoader.ins.loadVersion = version;
            EmptySceneLoader.ins.sceneToLoad = "BookShelf";
            EmptySceneLoader.ins.isAssetBundle = true;


            //this is from launcher so
            EmptySceneLoader.ins.unloadUrl = PlayerPrefs.GetString("Launcher_url_key");
            EmptySceneLoader.ins.unloadVersion = PlayerPrefs.GetInt("Launcher_version_key");
            EmptySceneLoader.ins.unloadAll = false;

            SceneManager.LoadSceneAsync("empty");

            //LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle(PlayerPrefs.GetString("BookShelf_url_key"), PlayerPrefs.GetInt("BookShelf_version_key"));
            //StartCoroutine(loader.IEStreamAssetBundle());

        }

    }


    private void FindingServer()
    {
        //Debug.Log(string.Format("finding server counting={0}", findingServerTime));
        pb.TextTitle.text = "Finding server " + findingServerTime.ToString() + "s";
        findingServerTime += 1;
        if (findingServerTime >= 60)
        {
            Debug.Log("What to do?");
        }
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            MessageBox.ins.ShowOk("No connection. Do something here.", MessageBox.MsgIcon.msgError, null);
            //stop lNet.FindServer();
        }
    }

    bool CheckBundleVersion(int serverBundleVersion)
    {
        if (serverBundleVersion > PlayerPrefs.GetInt("bundleVersion"))
        {
            return true;
        }
        return false;
    }

    bool CheckBundleCollectionBatchNumber(int n)
    {
        if (n > PlayerPrefs.GetInt("bundleCollectionBatchNumber"))
        {
            return true;
        }
        return false;
    }


    #region Messages

    void ConnectionErrMsgRetry(AssetBundleData assetBundleData)
    {
        MessageBox.ins.ShowQuestion("Retry download?", MessageBox.MsgIcon.msgInformation, () => { RetryDownload(assetBundleData); }, new UnityAction(BeforeCloseMsg));
    }

    void RetryDownload(AssetBundleData assetBundleData)
    {
        retryCount = 0;
        StartCoroutine(IEDownload(assetBundleData));
    }

    void BeforeCloseMsg()
    {
        MessageBox.ins.ShowOk("Try downloading your books later.", MessageBox.MsgIcon.msgInformation, null);
    }
    #endregion
}
