using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Assetbundle
{
    public class AssetBundleRef
    {
        public AssetBundle assetBundle = null;
        public int version;
        public string url;
        public AssetBundleRef(string strUrlIn, int intVersionIn)
        {
            url = strUrlIn;
            version = intVersionIn;
        }
    }

    static public class AssetBundleManager
    {

        // A dictionary to hold the AssetBundle references
        static private Dictionary<string, AssetBundleRef> dictAssetBundleRefs;


        static AssetBundleManager()
        {
            dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();
        }
    
   
        // Get an AssetBundle
        public static AssetBundle getAssetBundle(string url, int version)
        {
            string keyName = url + version.ToString();
            AssetBundleRef abRef;
            if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
                return abRef.assetBundle;
            else
                return null;
        }
        // Download an AssetBundle
        public static IEnumerator downloadAssetBundle(string url, int version)
        {
            string keyName = url + version.ToString();
            if (dictAssetBundleRefs.ContainsKey(keyName))
                yield return null;
            else
            {
                while (!Caching.ready)
                    yield return null;

                using (WWW www = WWW.LoadFromCacheOrDownload(url, version))
                {
                    yield return www;
                    if (www.error != null)
                        throw new Exception("WWW download:" + www.error);
                    AssetBundleRef abRef = new AssetBundleRef(url, version);
                    abRef.assetBundle = www.assetBundle;
                    dictAssetBundleRefs.Add(keyName, abRef);
                }
            }
        }

        public static bool ContainsKey(string url, int version)
        {
            string keyName = url + version.ToString();
            if (dictAssetBundleRefs.ContainsKey(keyName))
            {
                return true;
            }
            return false;
        }

        public static void Add(AssetBundleRef abr)
        {
            string key = abr.url + abr.version.ToString();
            dictAssetBundleRefs.Add(key, abr);
        }
        // Unload an AssetBundle
        public static void Unload(string url, int version, bool allObjects)
        {
            string keyName = url + version.ToString();
            AssetBundleRef abRef;
            if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
            {
                string[] scenePaths = abRef.assetBundle.GetAllScenePaths();
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
                Debug.Log("Unloaded! " + sceneName);
                abRef.assetBundle.Unload(allObjects);
                abRef.assetBundle = null;
                dictAssetBundleRefs.Remove(keyName);
            }
        }
    }
}