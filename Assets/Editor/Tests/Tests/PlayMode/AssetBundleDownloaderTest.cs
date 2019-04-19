using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using LitJson;
using _Assetbundle;

[UnityPlatform(exclude = new[] {RuntimePlatform.Android})]
public class AssetBundleDownloaderTest
{
#if UNITY_EDITOR
    [UnityTest]
    public IEnumerator DownLoadAssetBundle_DownLoadAssetBundleInServer_ReturnContentInJsonAsStringList()
    {
        GameObject gameObject = new GameObject();

        var assetBundleManifest = new AssetBundleManifest();
        assetBundleManifest.url = "http://127.0.0.1/AssetBundles";
        assetBundleManifest.version = "1.0.0";

        var assetBundleList = JsonMapper.ToObject<AssetBundleList>("{'assetBundles':['abc_circus','abccircus_act1']}");
        var assetBundleDownloader = gameObject.AddComponent<AssetBundleDownloader>();
        assetBundleDownloader.DownloadAssetBundle(assetBundleManifest.url, assetBundleManifest.version,
            assetBundleList.assetBundles);
        yield return null;
    }
#endif
}


