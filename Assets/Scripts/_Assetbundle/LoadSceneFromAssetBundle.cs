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

    AssetBundleRef abref;

    public LoadSceneFromAssetBundle(string urlBundle, int versionBundle)
    {
        sceneURL = urlBundle;
        version = versionBundle;

        abref = new AssetBundleRef(sceneURL, version);
    }



    public IEnumerator IEStreamAssetBundle()
    {
       
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
            if (AssetBundleManager.ContainsKey(sceneURL, version))
            {
                assetBundle = AssetBundleManager.getAssetBundle(sceneURL, version);
                LoadScene(assetBundle);
                yield return null;
            }
            else
            {
                while (!Caching.ready)
                {
                    yield return null;
                }
                using (WWW www = WWW.LoadFromCacheOrDownload(sceneURL, version))
                {
                    while (!www.isDone)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    Debug.Log(sceneURL + " " + version);
                    //assetBundle = www.assetBundle;
                   
                    abref.assetBundle = www.assetBundle;
                    AssetBundleManager.Add(abref);
                }

                LoadScene(abref.assetBundle);
            }
           
        }

    }

    void LoadScene(AssetBundle assetBundle)
    {
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
