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
                Name = StoryBook.HUGIS_KAY_SARAP
            };
            bad.activities.Add(new ActivityModelJson()
            {
                BookId = 11,
                Name = "book_test_1_Act1",
                Module = Module.WORD,
                Set = 0
            });
            bad.activities.Add(new ActivityModelJson()
            {
                BookId = 11,
                Name = "book_test_1_Act1",
                Module = Module.WORD,
                Set = 3
            });
            bad.activities.Add(new ActivityModelJson()
            {
                BookId = 11,
                Name = "book_test_1_Act1",
                Module = Module.WORD,
                Set = 6
            });
            bad.activities.Add(new ActivityModelJson()
            {
                BookId = 11,
                Name = "book_test_1_Act1",
                Module = Module.WORD,
                Set = 9
            });
            bad.activities.Add(new ActivityModelJson()
            {
                BookId = 11,
                Name = "book_test_1_Act1",
                Module = Module.WORD,
                Set = 12
            });
            
            var json = JsonUtility.ToJson(bad);
            //PlayerPrefs.SetString("bad", json);
            Debug.Log(json);
            //bad = JsonUtility.FromJson<BookAndActivityData>(PlayerPrefs.GetString("bad"));
        }
    }
}