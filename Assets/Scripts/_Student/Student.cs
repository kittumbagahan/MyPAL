using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Student : MonoBehaviour {

    public int id;
    public string name;

    public void Click()
    {
        MessageBox.ins.ShowQuestion("Load student " + name + "?", MessageBox.MsgIcon.msgInformation, new UnityAction(LoadYes), new UnityAction(LoadNo));
    }

    void LoadYes()
    {
        StoryBookSaveManager.ins.activeUser = name;
        StoryBookSaveManager.ins.activeUser_id = id;
        StudentController.ins.Close();
        print("Hey " + name.Split(' ')[3]);
        if (UnityEngine.Random.Range(0, 5) > 2)
        {
            Tammytam.ins.Say("Hello, \n" + name.Split(' ')[3] + "!");
        }
        else
        {
            Tammytam.ins.Say("Let's read \n" + name.Split(' ')[3] + "!");
        }
    }

    void LoadNo()
    {

    }
}
