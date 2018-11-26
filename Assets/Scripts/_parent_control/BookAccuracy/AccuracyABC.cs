using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyABC : BookAccuracy
{

  
    void OnEnable()
    {
        total = GetAccuracy();
    }
    
    public override double GetAccuracy()
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(StoryBook.ABC_CIRCUS.ToString(), "ABCCircus_Act2", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.ABC_CIRCUS.ToString(), "ABCCircus_Act2", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.ABC_CIRCUS.ToString(), "ABCCircus_Act2", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.ABC_CIRCUS.ToString(), "ABCCircus_Act2", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.ABC_CIRCUS.ToString(), "ABCCircus_Act2", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.ABC_CIRCUS.ToString(), "ABCCircus_Act1", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.ABC_CIRCUS.ToString(), "ABCCircus_Act1", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.ABC_CIRCUS.ToString(), "ABCCircus_Act1", Module.OBSERVATION.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.ABC_CIRCUS.ToString(), "ABCCircus_Act1", Module.OBSERVATION.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.ABC_CIRCUS.ToString(), "ABCCircus_Act1", Module.OBSERVATION.ToString(), 12));

        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "0")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "3")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "6")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "9")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "12")));

        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "0")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "3")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "6")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "9")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "12")));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

}
