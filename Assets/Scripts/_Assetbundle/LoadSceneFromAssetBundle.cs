using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFromAssetBundle : MonoBehaviour {

   AssetBundle assetBundle;
   [SerializeField]
   string sceneURL;
	void Start () {
      Caching.ClearCache ();
      StartCoroutine (IEDownload());
	}
	
	
   IEnumerator IEDownload()
   {
      //Download asset bundle
      WWW bundleWWW = WWW.LoadFromCacheOrDownload (sceneURL, 1);
      yield return bundleWWW;
      assetBundle = bundleWWW.assetBundle;

      if (assetBundle.isStreamedSceneAssetBundle)
      {
         string[] scenePaths = assetBundle.GetAllScenePaths ();
         string sceneName = System.IO.Path.GetFileNameWithoutExtension (scenePaths[0]);
         SceneManager.LoadScene (sceneName);
      }
   }
}
