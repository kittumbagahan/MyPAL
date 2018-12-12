using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFromAssetBundle
{

    AssetBundle assetBundle;
   
    string sceneURL;
    int version = 0;

    public LoadSceneFromAssetBundle(string urlBundle, int versionBundle)
    {
        sceneURL = urlBundle;
        version = versionBundle;
    }

   
    
    public IEnumerator IEStreamAssetBundle()
    {
        //Download asset bundle
      
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
            assetBundle = www.assetBundle;
        }
       
       

        if (assetBundle.isStreamedSceneAssetBundle)
        {
            string[] scenePaths = assetBundle.GetAllScenePaths();
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
