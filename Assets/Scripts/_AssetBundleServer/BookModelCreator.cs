using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BookModelCreator : MonoBehaviour {

    public BookAndActivityData bad = new BookAndActivityData();

    private void Start()
    {

        bad = JsonUtility.FromJson<BookAndActivityData>(PlayerPrefs.GetString("bad"));

        //Selected book enum is in capital letters
        //this one is the name of the scene so... 
        bad.book = new BookModelJson(11, StoryBook.BOOK_TEST_1.ToString());
        bad.lstActivity.Add(new ActivityModelJson(11, "Book_Test_1_Act1", Module.WORD, 0));
        bad.lstActivity.Add(new ActivityModelJson(11, "Book_Test_1_Act1", Module.WORD, 3));
        bad.lstActivity.Add(new ActivityModelJson(11, "Book_Test_1_Act1", Module.WORD, 6));
        bad.lstActivity.Add(new ActivityModelJson(11, "Book_Test_1_Act1", Module.WORD, 9));
        bad.lstActivity.Add(new ActivityModelJson(11, "Book_Test_1_Act1", Module.WORD, 12));

        string json = JsonUtility.ToJson(bad);
        PlayerPrefs.SetString("bad", json);
        Debug.Log(json);

    }
}
