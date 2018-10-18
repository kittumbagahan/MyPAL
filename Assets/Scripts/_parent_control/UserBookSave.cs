using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UserBookSave : MonoBehaviour {

	//REFERENCE FROM CarouItem.cs StoryBookSaveManager.instance.selectedBookName = sceneToLoad;
				
   
    public void UpdateReadUsage()
    { 
       //reading key
        string key = "read" + "section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + StoryBookSaveManager.ins.activeUser_id
          + StoryBookSaveManager.ins.selectedBook;
        //print("Read Usage " + key);
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1);
    }

    public void UpdateReadItToMeUsage()
    {
        //2018 08 30//string key = "readItToMe" + StoryBookSaveManager.ins.User + StoryBookSaveManager.ins.selectedBook;
        string key = "readItToMe" + "section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + StoryBookSaveManager.ins.activeUser_id
            + StoryBookSaveManager.ins.selectedBook;
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1);
        //print("Read Usage " + key);
    }

    public void UpdateAutoReadUsage()
    {
        //2018 08 30//string key = "auto" + StoryBookSaveManager.ins.User + StoryBookSaveManager.ins.selectedBook;
        string key = "auto" + "section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + StoryBookSaveManager.ins.activeUser_id
            + StoryBookSaveManager.ins.selectedBook;
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + 1);
        //print("Read Usage " + key);
    }


 

}
