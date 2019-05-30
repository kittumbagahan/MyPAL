using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using LitJson;
using UnityEngine.UI;
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
    
        var	assetBundleList = JsonMapper.ToObject<AssetBundleList>("{'assetBundles':['bookshelf','abc_circus']}");
        var assetBundleDownloader = gameObject.AddComponent<AssetBundleDownloader>();
        var progressBar = gameObject.AddComponent<Image>();
        var downloadDialog = gameObject.AddComponent<DownloadDialog>();
        progressBar.type = Image.Type.Filled;
        progressBar.fillMethod = Image.FillMethod.Horizontal;
        
        assetBundleDownloader.DownloadAssetBundle(assetBundleManifest, downloadDialog);
        yield return new WaitForSeconds(5);        
    }   
}

