using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BookModelCreator : MonoBehaviour {

    public BookAndActivityData bad = new BookAndActivityData();

    private void Start()
    {

        bad = JsonUtility.FromJson<BookAndActivityData>(PlayerPrefs.GetString("bad"));

        //bad.lstBooks.Add(new BookModelJson(11, "Book_Test_1"));
        //bad.lstActivity.Add(new ActivityModelJson(11, "Book_Test_1_Act1", Module.WORD, 0));
        //bad.lstActivity.Add(new ActivityModelJson(11, "Book_Test_1_Act1", Module.WORD, 3));
        //bad.lstActivity.Add(new ActivityModelJson(11, "Book_Test_1_Act1", Module.WORD, 6));
        //bad.lstActivity.Add(new ActivityModelJson(11, "Book_Test_1_Act1", Module.WORD, 9));
        //bad.lstActivity.Add(new ActivityModelJson(11, "Book_Test_1_Act1", Module.WORD, 12));

        string json = JsonUtility.ToJson(bad);
        PlayerPrefs.SetString("bad", json);
        Debug.Log(json);

    }
}
