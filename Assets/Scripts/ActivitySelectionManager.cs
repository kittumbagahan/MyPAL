using UnityEngine;
using System.Collections;

public class ActivitySelectionManager : MonoBehaviour {
    
    [SerializeField] private GameObject _canvas;

    SceneLoader _sceneLoader;

	private void Start () {
        _canvas = GameObject.Find("Canvas_UI_New");
        //use for going back to active storybook main page
        _sceneLoader = _canvas.GetComponent<SceneLoader>();
        _sceneLoader.SceneToLoad = StoryBookSaveManager.ins.GetBookScene();
        _sceneLoader.IsAssetBundle = AssetBundleInfo.BookScene.isAssetBundle;
        _sceneLoader.VersionKey = AssetBundleInfo.BookScene.versionKey;
        _sceneLoader.UrlKey = AssetBundleInfo.BookScene.urlKey;

        EmptySceneLoader.ins.isAssetBundle = true;
        EmptySceneLoader.ins.unloadAll = false;
        EmptySceneLoader.ins.unloadUrl = PlayerPrefs.GetString("ActivitySelection_url_key");
        EmptySceneLoader.ins.unloadVersion = PlayerPrefs.GetInt("ActivitySelection_version_key");
        

        Debug.Log("CHECK OUT FOR THIS");
		BG_Music.ins.SetVolume(0.5f);
	}
}
