using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetBundleNameGenerator : UnityEditor.Editor {

	[MenuItem("MyPAL/Asset Bundle/Assign Asset Bundle Name")]
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

	[MenuItem("MyPAL/Asset Bundle/Build Selected")]
	public static void BuildSelected()
	{
		// Get all selected *assets*
		var assets = Selection.objects.Where(o => !string.IsNullOrEmpty(AssetDatabase.GetAssetPath(o))).ToArray();
            
		List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();
		HashSet<string> processedBundles = new HashSet<string>();

		// Get asset bundle names from selection
		foreach (var o in assets)
		{
			var assetPath = AssetDatabase.GetAssetPath(o);
			var importer = AssetImporter.GetAtPath(assetPath);

			if (importer == null)
			{
				continue;
			}

			// Get asset bundle name & variant
			var assetBundleName = importer.assetBundleName;
			var assetBundleVariant = importer.assetBundleVariant;
			var assetBundleFullName = string.IsNullOrEmpty(assetBundleVariant) ? assetBundleName : assetBundleName + "." + assetBundleVariant;
                
			// Only process assetBundleFullName once. No need to add it again.
			if (processedBundles.Contains(assetBundleFullName))
			{
				continue;
			}

			processedBundles.Add(assetBundleFullName);
                
			AssetBundleBuild build = new AssetBundleBuild();

			build.assetBundleName = assetBundleName;
			build.assetBundleVariant = assetBundleVariant;
			build.assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleFullName);
                
			assetBundleBuilds.Add(build);
		}
		
		Debug.Log(assetBundleBuilds.Count);
		BuildPipeline.BuildAssetBundles("Assets/AssetBundles", assetBundleBuilds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.Android);
		Debug.Log("Done");
	}
}
