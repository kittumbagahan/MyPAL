using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class BookshelfLoader : MonoBehaviour
{

    void Start()
    {
        //on first run
        if (PlayerPrefs.GetString("BookShelf_url_key").Equals(""))
        {
            SceneManager.LoadSceneAsync("BookShelf");
        }
        else
        {
            Debug.Log(PlayerPrefs.GetString("BookShelf_url_key"));
            LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle(PlayerPrefs.GetString("BookShelf_url_key"), PlayerPrefs.GetInt("BookShelf_version_key"));
            loader.OnLoadSceneFail += FailLoadBookShelf;
            loader.OnLoadSceneSuccess += SuccessLoadBookShelf;

            StartCoroutine(loader.IEStreamAssetBundle());
        }


    }


    void FailLoadBookShelf()
    {

        Debug.Log("bookshelf loaded default");
        SceneManager.LoadSceneAsync("BookShelf");
    }
    void SuccessLoadBookShelf()
    {
        Debug.Log("bookshelf loaded assetbundle");


    }
}
