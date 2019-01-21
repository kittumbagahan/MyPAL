using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetBundleData
{

   public string url;
   public int version;
   public AssetBundleCategory assetCategory;
   public int patchBatchNumber; //patch batch
   public string description = "optional";
}
