using LitJson;
using UnityEngine;

public class Read : MonoBehaviour
{
    public static Read Instance;
    
    JsonData _data;
    
    private const string RPC_ASSET_NAME = "StoryBookActivityScene";   
    
    // Use this for initialization
    void Awake()
    {
        Instance = this;
       
        string fileInfo = PlayerPrefs.GetString("StoryBookActivityScene");

        if ("".Equals(fileInfo))
            fileInfo = GetTexTAsset();                

        _data = JsonMapper.ToObject(fileInfo);                
    }

    private string GetTexTAsset()
    {                                
        return Resources.Load<TextAsset>(RPC_ASSET_NAME).text;
    }   
    
    //"BUTTON INDEX" IS USE TO DIVIDE ONE ACTIVITY TO MANY
    //INDEX IN "StoryBookActivityScene.JSON" IS USE AS A SET INDEX TO SET AN ACTIVITY START UP INDEX
    //get the scene to be loaded
    public string SceneName(StoryBook storyBook, Module module, int buttonIndex)
    {
        return _data[storyBook + "_" + module][buttonIndex]["scene"].ToString();
    }

    //get the index of the scene
    public int SceneIndex(StoryBook storyBook, Module module, int buttonIndex)
    {
        return (int)_data[storyBook + "_" + module][buttonIndex]["index"];
    }
}
