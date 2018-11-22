using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyColorsAllMixedUp : BookAccuracy {

   
     void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 9));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_7", Module.WORD.ToString(), 12));

        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 1));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 2));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.COLORS_ALL_MIXED_UP.ToString(), "colorsAllMixedUp_Act_1", Module.OBSERVATION.ToString(), 4));

        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "0")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "3")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "6")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "9")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "12")));

        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "0")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "1")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "2")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "3")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "4")));

        SetList(lstGrade);
        return base.GetAccuracy();
    }
}
