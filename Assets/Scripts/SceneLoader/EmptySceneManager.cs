using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmptySceneManager : MonoBehaviour
{
    private static AssetBundle _assetBundle;

    void Start()
    {
        LoadScene();
    }   

    private void LoadScene()
    {
        SceneManager.LoadSceneAsync(EmptySceneLoader.ins.sceneToLoad, LoadSceneMode.Single);
    }
}