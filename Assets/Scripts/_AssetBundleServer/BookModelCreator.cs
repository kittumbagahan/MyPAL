using System;
using UnityEngine;

namespace _AssetBundleServer
{
    [Serializable]
    public class BookModelCreator : MonoBehaviour
    {
        public BookAndActivityData bad = new BookAndActivityData();

        private void Start()
        {
            //Selected book enum is in capital letters
            //this one is the name of the scene so... 
            bad.book = new BookModelJson(11, StoryBook.BOOK_TEST_1.ToString());
            bad.lstActivity.Add(new ActivityModelJson(11, "book_test_1_Act1", Module.WORD, 0));
            bad.lstActivity.Add(new ActivityModelJson(11, "book_test_1_Act1", Module.WORD, 3));
            bad.lstActivity.Add(new ActivityModelJson(11, "book_test_1_Act1", Module.WORD, 6));
            bad.lstActivity.Add(new ActivityModelJson(11, "book_test_1_Act1", Module.WORD, 9));
            bad.lstActivity.Add(new ActivityModelJson(11, "book_test_1_Act1", Module.WORD, 12));

            var json = JsonUtility.ToJson(bad);
            PlayerPrefs.SetString("bad", json);
            Debug.Log(json);
            //bad = JsonUtility.FromJson<BookAndActivityData>(PlayerPrefs.GetString("bad"));
        }
    }
}