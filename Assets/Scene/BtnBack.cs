using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BtnBack : MonoBehaviour {

	public string sceneToLoad = "BookShelf";
	
	public void Click()
	{
		//CachedAssetBundle.ins.UnloadBundle();
		//EmptySceneLoader.ins.sceneToLoad = "";// sceneToLoad;
		//SceneLoader.instance.AsyncLoadStr("empty");
        SceneManager.LoadSceneAsync(sceneToLoad);
	}
}
