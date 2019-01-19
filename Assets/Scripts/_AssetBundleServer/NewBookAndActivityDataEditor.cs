using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using SQLite4Unity3d;

public class NewBookAndActivityDataEditor : MonoBehaviour
{

    //Launcher will use this after downloading assetbundles
    void Start()
    {
        Debug.Log("HHOOY!");
        //ATTACHED ON LAUNCHER SCENE GAMEOBJECT

        //temporary reading from playerprefs
        BookAndActivityData bad = JsonUtility.FromJson<BookAndActivityData>(PlayerPrefs.GetString("bad"));

        DataService.Open("system/admin.db");
        int sectionsCount = DataService._connection.Table<AdminSectionsModel>().Count();
        DataService.Close();
        Debug.Log("Awwwii");
        for (int i = 1; i <= sectionsCount; i++)
        {
            Debug.Log("muuuu");
            DataService.Open("system/admin.db");
            string sectionName = DataService._connection.Table<AdminSectionsModel>().Where(x => x.Id == i).FirstOrDefault().Description;
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
        foreach (BookModel book in books)
        {
            Debug.Log(book.Description);
        }

        BookModel bm = DataService._connection.Table<BookModel>().Where(x => x.Description == bad.book.Description).FirstOrDefault();
        if (bm == null)
        {
            bm = new BookModel();
            bm.Description = bad.book.Description;
            DataService._connection.Insert(bm);

            bm = DataService._connection.Table<BookModel>().Where(x => x.Description == bad.book.Description).FirstOrDefault();

            for (int i = 0; i < bad.lstActivity.Count; i++)
            {
                ActivityModel am = new ActivityModel();
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