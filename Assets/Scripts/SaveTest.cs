using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SaveTest:MonoBehaviour{
    //SAVE ACTIVITY
    
    //these static variables are set on ButtonActivity.cs
    public static StoryBook storyBook;
    public static Module module;

    static int set;
	public static int Set
	{
        get { return set; }
        set { set = value; }
	}

    public static void Save()
    {        
		//print("USERNAME"+storyBook.ToString() + ", " + module.ToString() +  ", " + Set);
        //-----------------------------------------------------------------------------------add scene name here
		PlayerPrefs.SetString("section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + StoryBookSaveManager.ins.activeUser_id
            + storyBook.ToString() + SceneManager.GetActiveScene().name + module.ToString() + set, ScoreManager.ins.GetGrade());

        print("SAVED " + ScoreManager.ins.GetGrade());
        print(StoryBookSaveManager.ins.Username + storyBook.ToString() + SceneManager.GetActiveScene().name + module.ToString() + set);


        //PlayerPrefs.SetString("USERNAME" + storyBook.ToString() + module.ToString() + set, "done");
    }
    

}
