using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.IO;
using LitJson;
using UnityEngine.Networking;
using _Assetbundle;
	
[UnityPlatform(exclude = new[] {RuntimePlatform.Android})]
public class AssetBundleDownloaderTests {

    [UnityTest]
    public IEnumerator DownLoadAssetBundle_DownLoadAssetBundleInServer_ReturnContentInJsonAsStringList()	
    {
        GameObject gameObject = new GameObject();
    
        var assetBundleManifest = new AssetBundleManifest();
        assetBundleManifest.url = "http://192.168.0.103/AssetBundles";
        assetBundleManifest.version = "1.0.0";
    
        var	assetBundleList = JsonMapper.ToObject<AssetBundleList>("{'assetBundles':['bookshelf']}");
        var assetBundleDownloader = gameObject.AddComponent<AssetBundleDownloader>();		
        assetBundleDownloader.DownloadAssetBundle(assetBundleManifest.url, assetBundleManifest.version, assetBundleList.assetBundles);
        yield return new WaitForSeconds(5);
    }

    [UnityTest]
    public IEnumerator fcuk()
    {
        var uwr = new UnityWebRequest("http://192.168.0.103/AssetBundles/1.0.0/bookshelf", UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.dataPath + "/AssetBundles", "bookshelf");
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
            Debug.LogError(uwr.error);
        else
            Debug.Log("File successfully downloaded and saved to " + path);
    }
}

