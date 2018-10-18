﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyChatWithCat : BookAccuracy {

  
    void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "0")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "3")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "6")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "9")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "12")));

        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "0")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "1")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "2")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "3")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "4")));
        
       
        return base.GetAccuracy();
    }

}
