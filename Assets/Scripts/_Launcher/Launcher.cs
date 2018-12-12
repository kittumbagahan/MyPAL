using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public sealed class Launcher : CachedAssetBundleLoader
{

    [SerializeField]
    LauncherNetworking lNet;
    [SerializeField]
    ProgressBar pb;

    int findingServerTime = 0;
    private int retryCount;


    [SerializeField]
    string testUrl; //include extension name
    [SerializeField]
    int testVersion; //version not yet tested.. later..

    void Start()
    {
        //check for open network
        //if there is open network connect\
        lNet.Initialize();

        //lNet.OnFindingServer += FindingServer;
        //lNet.OnConnectedToServer += OnConnected;
        //lNet.FindServer();
        OnDownload += pb.SetProgress;

        bundleURL = testUrl;
        bundleVersion = testVersion;
        Caching.ClearCache();
        OnConnected();
    }

    private void OnConnected()
    {
        Debug.Log("I am connected");
        //wait for the server to send download url
        //automatically accept
        //download assetbundle from url
        StartCoroutine(IEDownload());
        //on download completed load the bundle
    }

    IEnumerator IEDownload()
    {
        yield return StartCoroutine(IEGetFromCacheOrDownload(bundleURL, 1));
        if (success)
        {
            PlayerPrefs.SetInt("bundleVersion", bundleVersion); // reference importanteu
            pb.TextTitle.text = "Download Complete";
            OnDownload -= pb.SetProgress;
            print("Complete download");
            if (bundle.isStreamedSceneAssetBundle)
            {
                string[] scenePaths = bundle.GetAllScenePaths();
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
                SceneManager.LoadScene(sceneName);
            }
        }
        else
        {
            if (retryCount < 20)
            {
                pb.TextTitle.text = "Connection error... Retrying download...";
                yield return new WaitForSeconds(1f);
                //stop exisiting download coroutine here
                retryCount++;
                StartCoroutine(IEDownload());
            }
            else
            {
                MessageBox.ins.ShowOk("INTERNET CONNECTION FAILED.", MessageBox.MsgIcon.msgError, new UnityAction(ConnectionErrMsgRetry));
            }
        }
    }



    private void FindingServer()
    {

        findingServerTime += 1;
        Debug.Log(string.Format("finding server counting={0}", findingServerTime));

        if (findingServerTime >= 60)
        {
            Debug.Log("What to do?");
        }
    }

    #region Messages

    void ConnectionErrMsgRetry()
    {
        MessageBox.ins.ShowQuestion("Retry download?", MessageBox.MsgIcon.msgInformation, new UnityAction(RetryDownload), new UnityAction(BeforeCloseMsg));
    }

    void RetryDownload()
    {
        retryCount = 0;
        StartCoroutine(IEDownload());
    }

    void BeforeCloseMsg()
    {
        MessageBox.ins.ShowOk("Try downloading your books later.", MessageBox.MsgIcon.msgInformation, null);
    }
    #endregion
}
