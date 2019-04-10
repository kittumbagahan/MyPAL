using UnityEngine;

#if UNITY_EDITOR
#endif

namespace _AssetBundleServer
{
    public class NewBookAndActivityDataEditor : MonoBehaviour
    {
        //Launcher will use this after downloading assetbundles
        private void Start()
        {
            Debug.Log("HHOOY!");
            //ATTACHED ON LAUNCHER SCENE GAMEOBJECT

            //temporary reading from playerprefs
            var bad = JsonUtility.FromJson<BookAndActivityData>(PlayerPrefs.GetString("bad"));

            DataService.Open("system/admin.db");
            var sectionsCount = DataService._connection.Table<AdminSectionsModel>().Count();
            DataService.Close();
            Debug.Log("Awwwii");
            for (var i = 1; i <= sectionsCount; i++)
            {
                Debug.Log("muuuu");
                DataService.Open("system/admin.db");
                var sectionName = DataService._connection.Table<AdminSectionsModel>().Where(x => x.Id == i)
                    .FirstOrDefault().Description;
                Debug.Log(sectionName);
                DataService.Close();
                AddNewBook(sectionName, bad);
            }
        }

        public void AddNewBook(string sectionDbName, BookAndActivityData bad)
        {
            DataService.Open(sectionDbName + ".db");
            var books = DataService._connection.Table<BookModel>();
            Debug.Log(books.Count());
            foreach (var book in books) Debug.Log(book.Description);

            var bm = DataService._connection.Table<BookModel>().Where(x => x.Description == bad.book.Description)
                .FirstOrDefault();
            if (bm == null)
            {
                bm = new BookModel();
                bm.Description = bad.book.Description;
                DataService._connection.Insert(bm);

                bm = DataService._connection.Table<BookModel>().Where(x => x.Description == bad.book.Description)
                    .FirstOrDefault();

                for (var i = 0; i < bad.lstActivity.Count; i++)
                {
                    var am = new ActivityModel();
                    am.BookId = bm.Id;
                    am.Description = bad.lstActivity[i].Description;
                    am.Module = bad.lstActivity[i].Module;
                    am.Set = bad.lstActivity[i].Set;

                    DataService._connection.Insert(am);
                }
            }
            else
            {
                Debug.Log("Book description already exist.");
                //throw new System.Exception("ERROR!");
            }

            DataService.Close();
        }
    }
}