using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmptySceneManager : MonoBehaviour
{
    private static AssetBundle _assetBundle;

    void Start()
    {
        GetAssetBundleScene();
    }

    private static void GetAssetBundleScene()
    {       
#if UNITY_EDITOR
        var url = Path.Combine(Application.dataPath + @"\AssetBundles\",
            EmptySceneLoader.ins.sceneToLoad.ToLower() + ".unity3d");
#elif UNITY_ANDROID
       var url =
 Path.Combine(Application.persistentDataPath + @"\AssetBundles\", EmptySceneLoader.ins.sceneToLoad.ToLower());
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
        
        // scene asset bundle contains only 1 scene each;
        var sceneName = scenes[0];
        
        Debug.Log("Load Scene :" + sceneName);
        Debug.Log("Scene count: " + _assetBundle.GetAllScenePaths().Length);
        
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}