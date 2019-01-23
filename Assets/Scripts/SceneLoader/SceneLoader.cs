using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{

    public static SceneLoader instance;
    [SerializeField]
    private string sceneToload;
    [SerializeField]
    GameObject loading;
    [SerializeField]
    bool isAssetBundle;
    [SerializeField]
    string assetBunldeUrlKey; //Beware of this. why? You'll find out soon.
    [SerializeField]
    string assetBundleUrlVersionKey;
    void Start()
    {
        instance = this;

    }

    public bool IsAssetBundle
    {
        set
        {
            isAssetBundle = value;
        }
    }


    public string UrlKey
    {
        set
        {
            assetBunldeUrlKey = value;
        }
    }

    public string VersionKey
    {
        set
        {
            assetBundleUrlVersionKey = value;
        }
    }

    public string SceneToLoad
    {
        set { sceneToload = value; }
        get { return sceneToload; }
    }


    public void AsyncLoadStr(string name)
    {
        if (loading != null) loading.gameObject.SetActive(true);
        if (isAssetBundle)
        {
            AssetBundleInfo.ActivityScene.urlKey = assetBunldeUrlKey;
            AssetBundleInfo.ActivityScene.versionKey = assetBundleUrlVersionKey;
            AssetBundleInfo.ActivityScene.isAssetBundle = isAssetBundle;
            AssetBundleInfo.ActivityScene.name = name;

            try
            {

                LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle(PlayerPrefs.GetString(assetBunldeUrlKey), PlayerPrefs.GetInt(assetBundleUrlVersionKey));
                loader.OnLoadSceneFail += Fail;
                loader.OnLoadSceneSuccess += Success;
                StartCoroutine(loader.IEStreamAssetBundle());
            }
            catch (LoadSceneFromAssetBundleException ex)
            {
                Debug.LogError("The book url key downloaded from assetbundle not found.\n Download try downloading the book again from the launcher.");
            }
           

        }
        else
        {
            if ("".Equals(name))
            {
                name = sceneToload;
            }
            SceneManager.LoadSceneAsync(name);

        }

    }

    void Fail()
    {
        Debug.Log("Loading scene fail.. Loading default");
        SceneManager.LoadSceneAsync(sceneToload);
    }

    void Success()
    {
        Debug.Log("Loading scene success");
    }

    void OnDestroy()
    {
        Item.RemoveSubscribers();
        ObjectToSpot.RemoveSubscribers();
    }
}
