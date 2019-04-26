using System.Collections.Generic;
using System.IO;
using LitJson;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	public class AssetBundleListAndAssetsEditor : UnityEditor.Editor {

		[MenuItem("MyPAL/Asset Bundle/Get All Asset Bundle Assets")]
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

		[MenuItem("MyPAL/Asset Bundle/Save All Asset Bundle To Json File")]
		static void SaveToJson()
		{
			var assetBundleList = new AssetBundleList();
			assetBundleList.assetBundles = new List<string>();
			foreach (var assetBundle in AssetDatabase.GetAllAssetBundleNames())
			{
				assetBundleList.assetBundles.Add(assetBundle);
			}

			var jsonString = JsonMapper.ToJson(assetBundleList).Replace('"', '\'');

			var path = EditorUtility.SaveFilePanel("Save file", "", "AssetBundleJson", "");

			if (path.Length != 0)
				File.WriteAllText(path, jsonString);
		}
	}
}
