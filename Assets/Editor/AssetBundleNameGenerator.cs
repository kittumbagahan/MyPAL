using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleNameGenerator : UnityEditor.Editor {

	[MenuItem("Assets/Assign Asset Bundle Name")]
	public static void GenerateAssetBundle()
	{		
		AssignAssetBundleName();
	}

	private static void AssignAssetBundleName()
	{
		if (Selection.objects.Length == 0)
		{
			EditorUtility.DisplayDialog("Asset Bundle Naming", "Nothing selected.", "OK");
			return;
		}

		foreach (var _object in Selection.objects)
		{
			var assetPath = AssetDatabase.GetAssetPath(_object);
			var assetImporter = AssetImporter.GetAtPath(assetPath);
			assetImporter.assetBundleName = Path.GetFileNameWithoutExtension(assetPath);
		}

		EditorUtility.DisplayDialog("Asset Bundle Naming", "Asset bundle naming successful.", "OK");
	}
}
