using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmptySceneManager : MonoBehaviour
{
   private static AssetBundle assetBundle;

    void Start()
    {
        AssetBundleManager.Unload(EmptySceneLoader.ins.unloadUrl, EmptySceneLoader.ins.unloadVersion, false);

      //if (EmptySceneLoader.ins.isAssetBundle)
      //{
      //    LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle(EmptySceneLoader.ins.loadUrl, EmptySceneLoader.ins.loadVersion);
      //    StartCoroutine(loader.IEStreamAssetBundle());
      //}
      //else
      //{
      //    SceneManager.LoadSceneAsync(EmptySceneLoader.ins.sceneToLoad);
      //}

      //F:\2018-2019_Projects\MyPAL\Assets\AssetBundles

         var url = Path.Combine(@"F:\2018-2019_Projects\MyPAL\Assets\AssetBundles\", EmptySceneLoader.ins.sceneToLoad.ToLower());

         if(assetBundle != null)
            assetBundle.Unload(false);

         assetBundle = AssetBundle.LoadFromFile(url);         

      if (assetBundle == null)
         {
            Debug.Log("Failed to load assetbundle");
            return;
         }         

         string[] scenes = assetBundle.GetAllScenePaths();

         if (scenes.Length > 0)
         {
            Debug.Log ("Load Scene :" + EmptySceneLoader.ins.sceneToLoad);                        
            Debug.Log ("Scene count: " + assetBundle.GetAllScenePaths ().Length);
            SceneManager.LoadSceneAsync(scenes[0], LoadSceneMode.Single);
            //assetBundle.Unload (false);
         }
         else
         {
            Debug.Log("No scene loaded");
         }
    }
}
