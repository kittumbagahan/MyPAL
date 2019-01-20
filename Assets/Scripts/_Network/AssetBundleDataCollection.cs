using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetBundleDataCollection {

    public List<AssetBundleData> lstAssetBundleData;
    
    public AssetBundleDataCollection()
    {
        lstAssetBundleData = new List<AssetBundleData>();
    }
}
