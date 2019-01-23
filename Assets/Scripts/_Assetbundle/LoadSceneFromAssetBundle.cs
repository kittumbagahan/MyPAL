using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadSceneFromAssetBundle
{

    AssetBundle assetBundle;

    string sceneURL;
    int version = 0;
    public delegate void LoadSceneFail();
    public event LoadSceneFail OnLoadSceneFail;
    public delegate void LoadSceneSuccess();
    public event LoadSceneSuccess OnLoadSceneSuccess;

    public LoadSceneFromAssetBundle(string urlBundle, int versionBundle)
    {
        sceneURL = urlBundle;
        version = versionBundle;
    }



    public IEnumerator IEStreamAssetBundle()
    {

        //try
        //{
        //    //Download asset bundle
        //    if (sceneURL.Equals(""))
        //    {

        //        MessageBox.ins.ShowOk("scene asset bundle url key is empty!", MessageBox.MsgIcon.msgError, null);
        //        throw new LoadSceneFromAssetBundleException("Scene URL is null", LoadSceneFromAssetBundleException.ErrorCode.MissingKey);
        //    }
        //}
        //catch(LoadSceneFromAssetBundleException ex)
        //{

        //}
        //Debug.LogError("BOOO");
        while (!Caching.ready)
        {
            yield return null;
        }
        if ("".Equals(sceneURL))
        {
            if (OnLoadSceneFail != null)
            {
                OnLoadSceneFail();

                OnLoadSceneFail = delegate { };
            }
        }
        else
        {
            using (WWW www = WWW.LoadFromCacheOrDownload(sceneURL, version))
            {
                while (!www.isDone)
                {
                    yield return new WaitForFixedUpdate();
                }
                Debug.Log(sceneURL + " " + version);
                assetBundle = www.assetBundle;
            }



            if (assetBundle.isStreamedSceneAssetBundle)
            {
                string[] scenePaths = assetBundle.GetAllScenePaths();
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
                if (OnLoadSceneSuccess != null)
                {
                    OnLoadSceneSuccess();
                    OnLoadSceneSuccess = delegate { };
                }
                SceneManager.LoadSceneAsync(sceneName);
            }
        }

    }
}
