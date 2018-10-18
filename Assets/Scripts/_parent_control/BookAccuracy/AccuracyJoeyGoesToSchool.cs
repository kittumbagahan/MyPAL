using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyJoeyGoesToSchool : BookAccuracy
{

  
     void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "0")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "3")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "6")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "9")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "12")));

        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "0")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "4")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "10")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "18")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "28")));
        
        SetList(lstGrade);
        return base.GetAccuracy();
    }

}
