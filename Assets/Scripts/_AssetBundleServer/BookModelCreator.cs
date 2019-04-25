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
            bad.book = new BookModelJson()
            {
                Id = 11,
                Description = StoryBook.HUGIS_KAY_SARAP.ToString()
            };
            bad.lstActivity.Add(new ActivityModelJson()
            {
                BookId = 11,
                Description = "book_test_1_Act1",
                Module = Module.WORD.ToString(),
                Set = 0
            });
            bad.lstActivity.Add(new ActivityModelJson()
            {
                BookId = 11,
                Description = "book_test_1_Act1",
                Module = Module.WORD.ToString(),
                Set = 3
            });
            bad.lstActivity.Add(new ActivityModelJson()
            {
                BookId = 11,
                Description = "book_test_1_Act1",
                Module = Module.WORD.ToString(),
                Set = 6
            });
            bad.lstActivity.Add(new ActivityModelJson()
            {
                BookId = 11,
                Description = "book_test_1_Act1",
                Module = Module.WORD.ToString(),
                Set = 9
            });
            bad.lstActivity.Add(new ActivityModelJson()
            {
                BookId = 11,
                Description = "book_test_1_Act1",
                Module = Module.WORD.ToString(),
                Set = 12
            });
            
            var json = JsonUtility.ToJson(bad);
            //PlayerPrefs.SetString("bad", json);
            Debug.Log(json);
            //bad = JsonUtility.FromJson<BookAndActivityData>(PlayerPrefs.GetString("bad"));
        }
    }
}