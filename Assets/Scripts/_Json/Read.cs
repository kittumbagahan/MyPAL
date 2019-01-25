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

    // Use this for initialization
    void Awake()
    {
        instance = this;


        //string fileInfo = Resources.Load<TextAsset>("StoryBookActivityScene").text;
        string fileInfo = PlayerPrefs.GetString("StoryBookActivityScene");

        if ("".Equals(fileInfo))
        {
            fileInfo = Resources.Load<TextAsset>("StoryBookActivityScene").text;
        }

        //Debug.Log ("file info " + fileInfo);

        data = JsonMapper.ToObject(fileInfo);
        Debug.Log(jsonString + "\n" + "READ SCRIPT " + gameObject.name);

        //Debug.Log (data);
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
