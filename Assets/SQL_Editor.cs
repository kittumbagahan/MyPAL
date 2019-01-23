using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR 
using UnityEditor;
using SQLite4Unity3d;

public class SQL_Editor {

    [MenuItem("Assets/Select Books")]
    static void SelectBooks()
    {
        DataService.Open();
        var book = DataService._connection.Table<BookModel>();//.Where(a => a.Description == "book_test_1").FirstOrDefault();

        var activityModel = DataService._connection.Table<ActivityModel>();//.Where(
        //     x => x.BookId == 11);
        //&&
        //     x.Description == "book_test_1" &&
        //     x.Module == "WORD" &&
        //     x.Set == 0).FirstOrDefault();

        foreach (var b in book)
        {
            Debug.Log(b.ToString());
        }

        foreach (var a in activityModel)
        {
            Debug.Log(a.ToString());
        }
    }
}
#endif