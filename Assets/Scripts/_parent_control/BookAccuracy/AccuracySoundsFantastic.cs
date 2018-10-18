using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracySoundsFantastic : BookAccuracy
{

    void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "0")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "3")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "6")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "9")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "12")));

        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act3" + Module.OBSERVATION + "0")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act6" + Module.OBSERVATION + "9")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act7" + Module.OBSERVATION + "0")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION + "-1")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION + "2")));

        SetList(lstGrade);
        return base.GetAccuracy();
    }
}
