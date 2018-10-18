using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class BackUpSaves : MonoBehaviour {

	[SerializeField]
	string path = "/";
	[SerializeField]
	List<string> lstData = new List<string>();
	string _userId;
	[SerializeField]
	int n=0; //read cntr

	[SerializeField]
	float loadSpd = 0.01f;

	[SerializeField]
	bool backUP;

	[SerializeField]
	TextMeshProUGUI tmLocation;
	[SerializeField]
	Text txtTime;
	[SerializeField]
	ProgressBar pb;

	IEnumerator coProcess;

	DirectoryInfo d;
	[SerializeField]
	FileInfo[] files;
	[SerializeField]
	List<string> fileNames;
	[SerializeField]
	TMP_Dropdown dropDownFiles;
	void Start () {
		d = new DirectoryInfo(Application.persistentDataPath);
		files = d.GetFiles("*.txt");
		for(int i=0; i<files.Length; i++)
		{
			fileNames.Add(files[i].ToString().Remove(0, Application.persistentDataPath.ToString().Length));
		}
		dropDownFiles.ClearOptions();
		dropDownFiles.AddOptions(fileNames);

		GetTime();
	}
	
	void GetTime()
	{
		txtTime.text = "Time until subscription expires " + ((TimeUsageCounter.ins.GetTime()/60)/60).ToString() + "hrs";
	}
	public void BackUpData()
	{
        string deyt = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_ss_tt");
		path = Application.persistentDataPath + "/backup" + deyt + ".txt";
		print(path);
        StartCoroutine(IEGetData());
	}

	void ClosePB()
	{
		pb.gameObject.SetActive(false);
	}

	IEnumerator IEProcess()
	{
		float progress=0, n;
		n = (float)1/(float)2130;
		pb.gameObject.SetActive(true);
		for(int i=0; i<2130; i++) //n is the number of lines
		{
			progress += n;
			//print(progress);
			pb.SetProgress(progress);
			yield return new WaitForSeconds(loadSpd);
		}
	}

	IEnumerator IEGetData()
	{
		coProcess = IEProcess();
		pb.TxtTitle.text = "Loading data...";
        //StartCoroutine(coProcess);
        int maxSectionAllowed = PlayerPrefs.GetInt("maxNumberOfSectionsAllowed");
        int maxStudentAllowed = PlayerPrefs.GetInt("maxNumberOfStudentsAllowed");
        for (int a=0; a<maxSectionAllowed; a++)
        {
            if (PlayerPrefs.GetString("section_id" + a.ToString()) != "")
            {
                string _sectionName = PlayerPrefs.GetString("section_id" + a.ToString());
                lstData.Add("section_id" + a.ToString() + "/" +_sectionName);
            }

            for (int b = 0; b < maxStudentAllowed; b++)
            {
                
                if (PlayerPrefs.GetString("section_id" + a.ToString() + "student_id" + b.ToString()) != "")
                {
                    _userId = "section_id" + a.ToString() + "student_id" + b.ToString();//Get(PlayerPrefs.GetString("sectiond_id" + a.ToString() + "student_id" + b.ToString()));
                    string _userName = PlayerPrefs.GetString(_userId);
                    yield return new WaitForSeconds(loadSpd);
                    print("user " + b + "=" + _userId + "/" + _userName);
					
                    lstData.Add(_userId + "/" + _userName);
                    yield return new WaitForSeconds(loadSpd);

                    //box
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act1_word" + Module.WORD + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act2_coloring" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act2_coloring" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act4" + Module.PUZZLE + "1" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act4" + Module.PUZZLE + "1")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act5" + Module.PUZZLE + "2" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act5" + Module.PUZZLE + "2")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favbox6_NEW" + Module.PUZZLE + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favbox6_NEW" + Module.PUZZLE + "3")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.FAVORITE_BOX.ToString() + "favBox_Act3_spotDiff" + Module.OBSERVATION + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    //cat
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_2" + Module.WORD + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_3" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_3" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_4" + Module.PUZZLE + "1" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_4" + Module.PUZZLE + "1")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_5" + Module.PUZZLE + "2" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_5" + Module.PUZZLE + "2")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_6" + Module.PUZZLE + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_6" + Module.PUZZLE + "3")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "1" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "1")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "2" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "2")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "4" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.CHAT_WITH_MY_CAT.ToString() + "chatWithCat_Act_1" + Module.OBSERVATION + "4")));
                    yield return new WaitForSeconds(loadSpd);

                    //colors
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_7" + Module.WORD + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_2" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_2" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_3" + Module.PUZZLE + "1" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_3" + Module.PUZZLE + "1")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_4" + Module.PUZZLE + "2" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_4" + Module.PUZZLE + "2")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_5" + Module.PUZZLE + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_5" + Module.PUZZLE + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_6" + Module.PUZZLE + "4" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_6" + Module.PUZZLE + "4")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "1" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "1")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "2" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "2")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "4" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.COLORS_ALL_MIXED_UP.ToString() + "colorsAllMixedUp_Act_1" + Module.OBSERVATION + "4")));
                    yield return new WaitForSeconds(loadSpd);

                    //rain
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act1" + Module.WORD + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act2" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act2" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act3" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act3" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act3" + Module.PUZZLE + "4" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act3" + Module.PUZZLE + "4")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act4" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act4" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "4" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "4")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "10" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "10")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "18" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "18")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "28" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.AFTER_THE_RAIN.ToString() + "afterTheRain_Act6" + Module.OBSERVATION + "28")));
                    yield return new WaitForSeconds(loadSpd);

                    //what
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct7" + Module.WORD + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act1" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act1" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct4" + Module.PUZZLE + "1" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct4" + Module.PUZZLE + "1")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct6" + Module.PUZZLE + "2" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct6" + Module.PUZZLE + "2")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct3" + Module.PUZZLE + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct3" + Module.PUZZLE + "3")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION + "-1" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION + "-1")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION + "2" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION + "2")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION + "5" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "whatDidYaSee_act2" + Module.OBSERVATION + "5")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct5" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct5" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct8" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.WHAT_DID_YOU_SEE.ToString() + "WhatDidYouSeeAct8" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);

                    //abc
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act2" + Module.WORD + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act4" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act4" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act6" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act6" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act5" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act5" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act5" + Module.PUZZLE + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act5" + Module.PUZZLE + "3")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.ABC_CIRCUS.ToString() + "ABCCircus_Act1" + Module.OBSERVATION + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    //joey
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act1" + Module.WORD + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act2" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act2" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act4" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act4" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act5" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act5" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act6" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act6" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "4" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "4")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "10" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "10")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "18" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "18")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "28" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.JOEY_GO_TO_SCHOOL.ToString() + "JoeyGoesToSchool_Act3" + Module.OBSERVATION + "28")));
                    yield return new WaitForSeconds(loadSpd);

                    //sounds
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "6"+ "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act4" + Module.WORD + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "soundsFantastic_Act1" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "soundsFantastic_Act1" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "soundsFantastic_Act2" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "soundsFantastic_Act2" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act5" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act5" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act5" + Module.PUZZLE + "4" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act5" + Module.PUZZLE + "4")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act3" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act3" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act6" + Module.OBSERVATION + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act6" + Module.OBSERVATION + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act7" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act7" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION + "-1" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION + "-1")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION + "2" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.SOUNDS_FANTASTIC.ToString() + "SoundsFantastic_Act8" + Module.OBSERVATION + "2")));
                    yield return new WaitForSeconds(loadSpd);

                    //tina and jun
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act1" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act1" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act2" + Module.WORD + "9")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act4" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act4" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act5" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act5" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act6" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act6" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act7" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act7" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "-1" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "-1")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "7" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "7")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "11" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "11")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "15" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.TINA_AND_JUN.ToString() + "TinaAndJun_Act3" + Module.OBSERVATION + "15")));
                    yield return new WaitForSeconds(loadSpd);

                    //yummy shapes
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD + "6" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD + "6")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD + "9" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD + "9")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD + "12" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_1" + Module.WORD + "12")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_4" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_4" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_5" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_5" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_6" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_6" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_7" + Module.PUZZLE + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_7" + Module.PUZZLE + "0")));
                    yield return new WaitForSeconds(loadSpd);

                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_2" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_2" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_2" + Module.OBSERVATION + "3" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_2" + Module.OBSERVATION + "3")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION + "0" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION + "0")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION + "4" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION + "4")));
                    yield return new WaitForSeconds(loadSpd);
                    lstData.Add(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION + "8" + "/" + Get(PlayerPrefs.GetString(_userId + StoryBook.YUMMY_SHAPES.ToString() + "yummyShapes_Act_3" + Module.OBSERVATION + "8")));
                    yield return new WaitForSeconds(loadSpd);
                }
            }
        }
      
		Debug.Log(path);
		StopCoroutine(coProcess);
		pb.gameObject.SetActive(false);
		Write();
	}

	void DeleteAllUserSaves(){
		int maxNumberOfStudentsAllowed = PlayerPrefs.GetInt("maxNumberOfStudentsAllowed");
		int maxNumberOfSectionsAllowed = PlayerPrefs.GetInt("maxNumberOfSectionsAllowed");
		string admin = PlayerPrefs.GetString("admin");
		int timeUsage = PlayerPrefs.GetInt("TimeUsage");
		string[] pwdResetCode = new string[10];
		for(int i=0; i<10; i++)
		{
			if(!PlayerPrefs.GetString("PWD_CODE_CHG" + i.ToString()).Equals(""))
			{
				pwdResetCode[i] = PlayerPrefs.GetString("PWD_CODE_CHG" + i.ToString());
			}
		}
		PlayerPrefs.DeleteAll();

	  	PlayerPrefs.SetInt("maxNumberOfStudentsAllowed", maxNumberOfStudentsAllowed);
		PlayerPrefs.SetInt("maxNumberOfSectionsAllowed", maxNumberOfSectionsAllowed);
		PlayerPrefs.SetString("admin", admin);
		PlayerPrefs.SetInt("TimeUsage", timeUsage);
		
		for(int i=0; i<10; i++)
		{
			PlayerPrefs.SetString("PWD_CODE_CHG", pwdResetCode[i]);
		}

	}
	void LoadData()
	{
		StartCoroutine(IELoadData());
	}

	IEnumerator IELoadData()
	{
		//int _userIndex = 0;
		n=0;
		pb.SetTitle("Loading data...");
		coProcess = IEProcess();
		StartCoroutine(coProcess);
        // int maxSectionAllowed = PlayerPrefs.GetInt("maxNumberOfSectionsAllowed");
        // int maxStudentAllowed = PlayerPrefs.GetInt("maxNumberOfStudentsAllowed");
        //string _sectionName, _studentName;
		DeleteAllUserSaves();
		yield return new WaitForSeconds(loadSpd); 
		for(int i=0; i<lstData.Count-1; i++){
			print("Data :" + lstData[i].Split('/')[0].ToString() + "/" + lstData[i].Split('/')[1].ToString());
			PlayerPrefs.SetString(lstData[i].Split('/')[0],lstData[i].Split('/')[1]);
			print("Checking value" + PlayerPrefs.GetString(lstData[i].Split('/')[0]));
			yield return new WaitForSeconds(loadSpd); 
		}
    
		StopCoroutine(coProcess);
		MessageBox.ins.ShowOk("Saved data loaded! \nRestart the app to update changes.", MessageBox.MsgIcon.msgInformation, new UnityAction(ClosePB));
		print("SAVES LOADED!");


	}

	string Get(string s)
	{
		if(s.Equals(""))
			return "0";
		return s;
	}

	public void Write()
	{
		print("creating the fille");
		//lstData.Clear();
		//PrintDatas();
		pb.gameObject.SetActive(true);
		//pb.TxtTitle.text = "Creating the back up file...";
		pb.SetTitle("Creating the back up file...");
		if(!File.Exists(path))
		{
			print("starting!");
			pb.SetTitle("STARTING... ");
            //print(path + " pathology");
            //File.CreateText(path);
            using (StreamWriter sw = File.CreateText(path))
            {
                pb.SetTitle("ALMOST THERE... ");
                print("almost there!");
                float lineCnt = (float)lstData.Count;
                for (int i = 0; i < lstData.Count; i++)
                {

                    pb.SetProgress(i / lineCnt);
                    sw.WriteLine(lstData[i]);
                    //print("in there!");
                }
                sw.WriteLine("endofline13XX");
            }
            MessageBox.ins.ShowOk("Back up created succesfully!", MessageBox.MsgIcon.msgInformation, new UnityAction(ClosePB));
		}
		else
		{
			MessageBox.ins.ShowOk("File already exist!", MessageBox.MsgIcon.msgError, new UnityAction(ClosePB));
			//File.Delete(path);

		}
		print("creating the file success! " + path);

	}

	public void Read()
	{
		if(!fileNames[dropDownFiles.value].Equals(""))
		StartCoroutine(IERead());
		else
		{
			MessageBox.ins.ShowOk("Select an option.", MessageBox.MsgIcon.msgError, null);
		}
	}

	IEnumerator IERead()
	{
		path = Application.persistentDataPath + "/" + fileNames[dropDownFiles.value];
        print("trying to load: " + path);
		if(File.Exists(path))
		{
			pb.gameObject.SetActive(true);
			pb.SetTitle("Reading data from the back up file...");
			coProcess = IEProcess();
			StartCoroutine(coProcess);
			StreamReader reader = new StreamReader(path);
			string _line = "";

			//lstData.Clear();
			while((_line = reader.ReadLine()) != "endofline13XX")
			{
				lstData.Add(_line);

				//n++;
				print("reading" + "line=" + _line);
				yield return new WaitForSeconds(loadSpd);
			}
			print("Saving read datas...");
			StopCoroutine(coProcess);
			pb.gameObject.SetActive(false);
			Invoke("LoadData", loadSpd);
		}
		else
		{
			MessageBox.ins.ShowOk("NO BACK UP FILE TO LOAD.", MessageBox.MsgIcon.msgError, null);
		}

	}
	
}
