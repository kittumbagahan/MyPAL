using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SQLite4Unity3d;

public class QuizAdmin : MonoBehaviour {

    DataService dataService;

    QuizModel quizModel;
    QuizItemModel quizItemModel;
    QuizItemChoicesModel quizItemChoicesModel;

    [SerializeField]
    GameObject quizListContent;


    // privates
    private QuizManager m_quizManager;

    public enum MaintenanceState
    {
        Add, Edit
    }

    // Use this for initialization
    void Start () {
        dataService = new DataService ();

        // check models
        CheckQuizModel ();
        CheckQuizItemModel ();
        CheckQuizItemChoicesModel ();
        CheckSubjectModel ();


        SetUp ();
    }

    #region DB

    void SetUp()
    {
        GetComponent<QuizManager> ().PopulateQuizList ();
    }

    void CheckQuizModel()
    {
        try
        {
            dataService._connection.Table<QuizModel> ().Count();
            Debug.Log ("Quiz Model Exist");
        }
        catch(SQLiteException ex)
        {
            dataService._connection.CreateTable<QuizModel> ();
        }
    }

    void CheckQuizItemModel()
    {
        try
        {
            dataService._connection.Table<QuizItemModel> ().Count ();
            Debug.Log ("Quiz Item Model Exist");
        }
        catch (SQLiteException ex)
        {
            dataService._connection.CreateTable<QuizItemModel> ();
        }
    }

    void CheckQuizItemChoicesModel ()
    {
        try
        {
            dataService._connection.Table<QuizItemChoicesModel> ().Count ();
            Debug.Log ("Quiz Item Choices Model Exist");
        }
        catch (SQLiteException ex)
        {
            dataService._connection.CreateTable<QuizItemChoicesModel> ();
        }
    }

    void CheckSubjectModel()
    {
        try
        {
            dataService._connection.Table<SubjectModel> ().Count ();
        }
        catch(SQLiteException ex)
        {
            dataService._connection.CreateTable<SubjectModel> ();
            SubjectModel subjectModel1 = new SubjectModel
            {
                Name = "MATH"
            };

            SubjectModel subjectModel2 = new SubjectModel
            {
                Name = "ENGLISH"
            };

            dataService._connection.Insert (subjectModel1);
            dataService._connection.Insert (subjectModel2);
        }
    }
    #endregion
}
