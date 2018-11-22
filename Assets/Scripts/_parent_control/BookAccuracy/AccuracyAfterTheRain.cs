using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyAfterTheRain : BookAccuracy {

     void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;

        lstGrade = new List<string>();
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act1", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 4));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 10));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 18));
        lstGrade.Add(GetGrade(StoryBook.AFTER_THE_RAIN.ToString(), "afterTheRain_Act6", Module.OBSERVATION.ToString(), 28));

        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "0")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "3")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "6")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "9")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "12")));

        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "0")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "4")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "10")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "18")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "28")));
      
        return base.GetAccuracy();
    }

}
