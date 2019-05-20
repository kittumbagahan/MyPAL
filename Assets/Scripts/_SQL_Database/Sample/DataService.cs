using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

using System.Threading;

public static class DataService
{

    public static SQLiteConnection _connection { private set; get; }

    public static void Open()
    {
        string DatabaseName; /* = PlayerPrefs.GetString("activeDatabase") == "" ? "tempDatabase.db" : PlayerPrefs.GetString("activeDatabase");*/
        DatabaseName = ActiveDatabase();
        string dbPath = Application.persistentDataPath + "/" + DatabaseName;

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        //Debug.Log("Open Final PATH: " + dbPath);
    }

    public static void Open(string DatabaseName)
    {
        string dbPath = Application.persistentDataPath + "/" + DatabaseName;
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Open Final PATH: " + dbPath);
    }

    public static void Close()
    {
        _connection.Close();
        _connection.Dispose();
        GC.Collect();
        //Debug.Log("Close connection");
    }

    public static string ActiveDatabase()
    {
        return PlayerPrefs.GetString("activeDatabase", "");       
    }

    public static void SetDbName(string pDbName)
    {
        PlayerPrefs.SetString("activeDatabase", pDbName);
    }   

    public static IEnumerable<SubscriptionTimeModel> GetSubscription()
    {
        return _connection.Table<SubscriptionTimeModel>();
    }

    public static IEnumerable<BookModel> GetBooks()
    {
        return _connection.Table<BookModel>();
    }

    public static IEnumerable<ActivityModel> GetActivities()
    {
        return _connection.Table<ActivityModel>();
    }

    public static IEnumerable<SectionModel> GetSections()
    {
        return _connection.Table<SectionModel>();
    }

    public static IEnumerable<StudentModel> GetStudents()
    {
        return _connection.Table<StudentModel>();
    }

    public static IEnumerable<StudentActivityModel> GetStudentActivities()
    {
        return _connection.Table<StudentActivityModel>();
    }

    public static IEnumerable<StudentBookModel> GetStudentBooks()
    {
        return _connection.Table<StudentBookModel>();
    }  
    

    public static Person CreatePerson()
    {
        var p = new Person
        {
            Name = "Johnny",
            Surname = "Mnemonic",
            Age = 21
        };
        _connection.Insert(p);
        return p;
    }

    public static StudentModel StudentModel(int id)
    {
        return _connection.Table<StudentModel>()
            .Where(student => student.Id == id)
            .FirstOrDefault();
    }
}
