using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetBundleDataCollection
{

   public List<AssetBundleData> lstAssetBundleData;
   public string batchId = "default";
   public int batchN = 0;
   public AssetBundleDataCollection()
   {
      lstAssetBundleData = new List<AssetBundleData> ();
   }
}
