using System;
using System.Collections;
using System.IO;
using LitJson;
using UnityEngine;

public class Read : MonoBehaviour
{

    public static Read instance;

    string jsonString;
    JsonData data;
    
    private const string AssetName = "StoryBookActivityScene.json";

    private TextAsset _storyBookActivityScene;

    private static AssetBundle _assetBundle;
    
    // Use this for initialization
    void Awake()
    {
        instance = this;


        //string fileInfo = Resources.Load<TextAsset>("StoryBookActivityScene").text;
        string fileInfo = PlayerPrefs.GetString("StoryBookActivityScene");

        if ("".Equals(fileInfo))
        {
            fileInfo = GetTexTAsset(AssetName);
        }
        
        //Debug.Log ("file info " + fileInfo);

        data = JsonMapper.ToObject(fileInfo);
        Debug.Log(jsonString + "\n" + "READ SCRIPT " + gameObject.name);

        //Debug.Log (data);
    }

    private string GetTexTAsset(string assetName)
    {                
        _assetBundle = GetAssetBundle(assetName);        
        return _assetBundle.LoadAsset<TextAsset>(assetName).text;
    }

    private AssetBundle GetAssetBundle(string fileName)
    {
#if UNITY_EDITOR      
        var url = Path.Combine (Application.dataPath + @"\AssetBundles\", Path.GetFileNameWithoutExtension(AssetName).ToLower());
#elif UNITY_ANDROID       
        var url = Path.Combine(Application.persistentDataPath + @"\AssetBundles\", Path.GetFileNameWithoutExtension(AssetName).ToLower());
#endif
        
        if(_assetBundle != null)
        _assetBundle.Unload(false);
            
        Debug.Log(Path.GetFileNameWithoutExtension(AssetName).ToLower());
        return AssetBundle.LoadFromFile(url);        
    }

    private void UnLoadAsset()
    {
        
    }
    
    //"BUTTON INDEX" IS USE TO DIVIDE ONE ACTIVITY TO MANY
    //INDEX IN "StoryBookActivityScene.JSON" IS USE AS A SET INDEX TO SET AN ACTIVITY START UP INDEX
    //get the scene to be loaded
    public string SceneName(StoryBook storyBook, Module module, int buttonIndex)
    {
        return data[storyBook + "_" + module][buttonIndex]["scene"].ToString();
    }

    //get the index of the scene
    public int SceneIndex(StoryBook storyBook, Module module, int buttonIndex)
    {
        return (int)data[storyBook + "_" + module][buttonIndex]["index"];
    }
}
