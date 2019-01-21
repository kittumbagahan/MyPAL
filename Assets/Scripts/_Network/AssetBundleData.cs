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
   public string description; //if is a book or activity fill this out with the book name case sensitive

   public AssetBundleData(string url, int version, AssetBundleCategory cat, int patchBatchN, string desc)
   {
      this.url = url;
      this.version = version;
      assetCategory = cat;
      patchBatchNumber = patchBatchN;
      description = desc;
   }
   public AssetBundleData()
   {

   }
}
