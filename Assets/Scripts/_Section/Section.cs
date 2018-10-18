using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Section : MonoBehaviour {

    public int id;
    public string name;

    public void Click()
    {
        MessageBox.ins.ShowQuestion("Load section " + name + "?", MessageBox.MsgIcon.msgInformation,new UnityAction(LoadYes), new UnityAction(LoadNo));
    }

    void LoadYes()
    {
        StoryBookSaveManager.ins.activeSection = name;
        StoryBookSaveManager.ins.activeSection_id = id;
        SectionController.ins.Close();
        StudentController.ins.LoadStudents();
    }

    void LoadNo()
    {

    }
}
