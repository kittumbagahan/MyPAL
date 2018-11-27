﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreateDBScript : MonoBehaviour
{

    public Text DebugText;

    // Use this for initialization
    void Start()
    {
        StartSync();
        DatabaseController dc = new DatabaseController();
       
    }

    private void StartSync()
    {
        //set the active database for the app for the firstime
        //if ("".Equals(PlayerPrefs.GetString("activeDatabase")))
        //{
        //    PlayerPrefs.SetString("activeDatabase", "tempDatabase.db");
        //}

        var ds = new DataService();
        ds.CreateDB();
        var books = ds.GetBooks();
        var acts = ds.GetActivities();
        var sections = ds.GetSections();
        var st = ds.GetStudents();
        var studentActivities = ds.GetStudentActivities();
        var studentBooks = ds.GetStudentBooks();
        ToConsole(books);
        //ToConsole(studentBooks);
        //ToConsole (books);
        //ToConsole (sections);
        //ToConsole (st);
        //ToConsole (acts);
        //var people = ds.GetPersons ();
        //ToConsole (people);
        //people = ds.GetPersonsNamedRoberto ();
        //ToConsole("Searching for Roberto ...");
        //ToConsole (people); 
    }

    private void ToConsole(IEnumerable<BookModel> model)
    {
        foreach (var person in model)
        {

            //ToConsole(person.ToString());
            Debug.Log(person.ToString());
        }
    }
    private void ToConsole(IEnumerable<ActivityModel> model)
    {
        foreach (var person in model)
        {
            //ToConsole(person.ToString());
            Debug.Log(person.ToString());
        }
    }
    private void ToConsole(IEnumerable<SectionModel> model)
    {
        foreach (var person in model)
        {
            //ToConsole(person.ToString());
            Debug.Log(person.ToString());
        }
    }

    private void ToConsole(IEnumerable<StudentModel> model)
    {
        foreach (var person in model)
        {
            //ToConsole(person.ToString());
            Debug.Log(person.ToString());
        }
    }

    private void ToConsole(IEnumerable<StudentActivityModel> model)
    {
        foreach (var person in model)
        {
            //ToConsole(person.ToString());
            Debug.Log(person.ToString());
        }
    }
    private void ToConsole(IEnumerable<StudentBookModel> model)
    {
        foreach (var person in model)
        {
            //ToConsole(person.ToString());
            Debug.Log(person.ToString());
        }
    }

    private void ToConsole(string msg)
    {
        DebugText.text += System.Environment.NewLine + msg;
        Debug.Log(msg);
    }
}
