using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyWhatDidYouSee : BookAccuracy
{

 
     void OnEnable()
    {
        total = GetAccuracy();
    }
    public override double GetAccuracy()
    {
        string _userId = "section_id" + StoryBookSaveManager.ins.activeSection_id.ToString() + "student_id" + UserAccountManager.ins.SelectedSlot.UserId;
        lstGrade = new List<string>();
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "0")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "3")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "6")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "9")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "12")));

        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION + "-1")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION + "2")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION + "5")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct5" + Module.OBSERVATION + "0")));
        lstGrade.Add(Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct8" + Module.OBSERVATION + "0")));

        SetList(lstGrade);
        return base.GetAccuracy();
    }
}
