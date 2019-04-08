using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	public class AssetBundleListAndAssetsEditor : UnityEditor.Editor {

		[MenuItem("Assets/Get All Asset Bundle Assets")]
		static void GetAssetWithAssetBundleName()
		{
			var objectSelection = new List<Object>();
			
			GetAssetBundle(objectSelection);

			Selection.objects = objectSelection.ToArray();
		}

		private static void GetAssetBundle(List<Object> objectSelection)
		{
			foreach (var assetBundle in AssetDatabase.GetAllAssetBundleNames())
			{
				GetAsset(assetBundle, objectSelection);
			}
		}

		private static void GetAsset(string assetBundle, List<Object> objectSelection)
		{
			foreach (var assetPathAndName in AssetDatabase.GetAssetPathsFromAssetBundle(assetBundle))
			{
				Debug.Log(assetPathAndName);
				objectSelection.Add(AssetDatabase.LoadMainAssetAtPath(assetPathAndName));
			}
		}
	}
}
