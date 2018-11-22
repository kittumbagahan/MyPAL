using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyTinaAndJun : BookAccuracy
{

 
    void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        //string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();

        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act1", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 0));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 6));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act2", Module.WORD.ToString(), 9));


        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), -1));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 3));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 7));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 11));
        lstGrade.Add(GetGrade(StoryBook.TINA_AND_JUN.ToString(), "TinaAndJun_Act3", Module.OBSERVATION.ToString(), 15));

        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act1" + Module.WORD + "0")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "0")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "3")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "6")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "9")));

        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "-1")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "3")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "7")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "11")));
        //lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "15")));

        SetList(lstGrade);
        return base.GetAccuracy();
    }

}
