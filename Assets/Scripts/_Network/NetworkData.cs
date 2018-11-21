using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkData {

    // for StudentActivityModel
    public int ID;
    public int sectionId;
    public int studentId;
    public int bookId;
    public int activityId;
    public string grade;
    public int playCount;

    // StudentBookModel
    public int book_Id;
    public int book_SectionId;
    public int book_StudentId;
    public int book_bookId;
    public int book_readCount;
    public int book_readToMeCount;
    public int book_autoReadCount;
}
