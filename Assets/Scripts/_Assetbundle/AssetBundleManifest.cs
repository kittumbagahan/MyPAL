using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class AssetBundleManifest
{

	[SerializeField]
	public string url;
	[SerializeField]
	public string version;	
	[SerializeField] public string assetBundleJson;
}