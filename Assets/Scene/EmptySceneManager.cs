using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmptySceneManager : MonoBehaviour
{


    void Start()
    {
        AssetBundleManager.Unload(EmptySceneLoader.ins.unloadUrl, EmptySceneLoader.ins.unloadVersion, false);

        if (EmptySceneLoader.ins.isAssetBundle)
        {
            LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle(EmptySceneLoader.ins.loadUrl, EmptySceneLoader.ins.loadVersion);
            StartCoroutine(loader.IEStreamAssetBundle());
        }
        else
        {
            SceneManager.LoadSceneAsync(EmptySceneLoader.ins.sceneToLoad);
        }


    }


}
