using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivityBack : MonoBehaviour {
   [SerializeField]
   bool isActivityAssetBundle;
   [SerializeField]
   string assetBunldeUrlKey;
   [SerializeField]
   string assetBundleUrlVersionKey;

  
   string sceneToLoad = "ActivitySelection";

    //this is not back to launcher but to ActivitySelection
    public void BackToLauncher()
   {
      if (isActivityAssetBundle)
      {
         string url = PlayerPrefs.GetString (assetBunldeUrlKey);
         int version = PlayerPrefs.GetInt (assetBundleUrlVersionKey);

         //back to activity selection
         EmptySceneLoader.ins.loadUrl = PlayerPrefs.GetString ("ActivitySelection_url_key");
         EmptySceneLoader.ins.loadVersion = PlayerPrefs.GetInt ("ActivitySelection_version_key");
         EmptySceneLoader.ins.sceneToLoad = sceneToLoad;
         EmptySceneLoader.ins.isAssetBundle = true;

         //unload activity itself
         EmptySceneLoader.ins.unloadUrl = url;
         EmptySceneLoader.ins.unloadVersion = version;
         EmptySceneLoader.ins.unloadAll = false;
      }
      //if activity is pre installed and activity selection assetbundle key is not null
      else
      {
         
         if(PlayerPrefs.GetString ("ActivitySelection_url_key").Equals (""))
         {
            EmptySceneLoader.ins.isAssetBundle = false;
            EmptySceneLoader.ins.sceneToLoad = sceneToLoad;
         }
         else
         {
            EmptySceneLoader.ins.loadUrl = PlayerPrefs.GetString ("ActivitySelection_url_key");
            EmptySceneLoader.ins.loadVersion = PlayerPrefs.GetInt ("ActivitySelection_version_key");
            EmptySceneLoader.ins.sceneToLoad = sceneToLoad;
            EmptySceneLoader.ins.isAssetBundle = true;
         }
      }
      SceneManager.LoadSceneAsync ("empty");

   }
	
	
}
