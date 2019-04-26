using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using _AssetBundleServer;

public class NewBookGenerator : EditorWindow
{

	public List<BookAndActivityData> bookAndActivityData;

	private SerializedObject _serializedObject;
	private SerializedProperty _books;

	private Vector2 scrollPosition;
	
	[MenuItem("MyPAL/New Book and Activity")]
	static void OpenWindow()
	{
		NewBookGenerator assetBundleManifestGenerator = GetWindow<NewBookGenerator>();
		
		assetBundleManifestGenerator.Show();
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height));
		
		GUILayout.Label("Asset Bundles");
		EditorGUILayout.PropertyField(_books, true);
		_serializedObject.ApplyModifiedProperties();												
		
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Generate Json"))
		{			
			var jsonString = JsonMapper.ToJson(bookAndActivityData).Replace('"', '\'');
            
            var path = EditorUtility.SaveFilePanel("Save file", "", "BookAndActivity", "");

            if (path.Length != 0)
            	File.WriteAllText(path, jsonString);
		}

		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndHorizontal();
	}

	private void OnEnable()
	{
		_serializedObject = new SerializedObject(this);
		_books = _serializedObject.FindProperty("bookAndActivityData");
	}
}
