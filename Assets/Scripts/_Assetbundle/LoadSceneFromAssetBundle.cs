using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFromAssetBundle : MonoBehaviour
{

    AssetBundle assetBundle;
    [SerializeField]
    string sceneURL;
    [SerializeField]
    int version = 1;
    void Start()
    {
        Caching.ClearCache();
        StartCoroutine(IEDownload());
    }



    IEnumerator IEDownload()
    {
        //Download asset bundle
        WWW bundleWWW;
        while (!Caching.ready)
        {
            yield return null;
        }
        using (bundleWWW = WWW.LoadFromCacheOrDownload(sceneURL, version))
        {
            while (!bundleWWW.isDone)
            {
                yield return new WaitForFixedUpdate();
            }
        }

        assetBundle = bundleWWW.assetBundle;

        if (assetBundle.isStreamedSceneAssetBundle)
        {
            string[] scenePaths = assetBundle.GetAllScenePaths();
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
            SceneManager.LoadScene(sceneName);
        }
    }
}
