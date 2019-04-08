using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmptySceneManager : MonoBehaviour
{
    private static AssetBundle _assetBundle;

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

        //var url = Path.Combine(@"F:\2018-2019_Projects\MyPAL\Assets\AssetBundles\", EmptySceneLoader.ins.sceneToLoad.ToLower());
#if UNITY_EDITOR      
        var url = Path.Combine (@"E:\Documents\UnityProjects\MyPAL\Assets\AssetBundles\", EmptySceneLoader.ins.sceneToLoad.ToLower ());
#elif UNITY_ANDROID       
       var url = Path.Combine(Application.persistentDataPath + @"\AssetBundles\", EmptySceneLoader.ins.sceneToLoad.ToLower());
#endif

        if (_assetBundle != null)
            _assetBundle.Unload(false);

        _assetBundle = AssetBundle.LoadFromFile(url);         

        if (_assetBundle == null)
        {
            Debug.Log("Failed to load assetbundle");
            return;
        }         

        var scenes = _assetBundle.GetAllScenePaths();

        if (scenes.Length <= 0)
        {
            Debug.Log("No scene loaded");
            return;                        
            //assetBundle.Unload (false);
        }
        
        Debug.Log ("Load Scene :" + EmptySceneLoader.ins.sceneToLoad);                        
        Debug.Log ("Scene count: " + _assetBundle.GetAllScenePaths ().Length);
        SceneManager.LoadSceneAsync(scenes[0], LoadSceneMode.Single);
    }
}