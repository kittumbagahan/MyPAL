using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeacherLogIn : MonoBehaviour {

	[SerializeField]
	TMP_InputField inputPWD;

	void Start () {
        if(StoryBookSaveManager.ins.selectedBook != StoryBook.NULL)
        {
            gameObject.SetActive(false);
        }
		FirstRun();
	}
	
	void FirstRun()
	{
		if(PlayerPrefs.GetInt("Maintenance_first_run") != 1)
		{
			PlayerPrefs.SetString("admin","1234");
			PlayerPrefs.SetInt("Maintenance_first_run", 1);

		}
	}

	public void LogIn()
	{
		if(PlayerPrefs.GetString("admin") == inputPWD.text){
            UserRestrictionController.ins.Restrict(0);
            StoryBookSaveManager.ins.activeUser = "teacher";
            StoryBookSaveManager.ins.activeUser_id = -1;
            gameObject.SetActive(false);
            //show section selection
		}
		else
		{
			MessageBox.ins.ShowOk("Access denied!", MessageBox.MsgIcon.msgError, null);
		}
	}


}
