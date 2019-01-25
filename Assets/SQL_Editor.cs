using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR 
using UnityEditor;
using SQLite4Unity3d;

public class SQL_Editor
{



    [MenuItem("Assets/Select Books")]
    static void SelectBooks()
    {
        DataService.Open();
        var book = DataService._connection.Table<BookModel>();//.Where(a => a.Description == "book_test_1").FirstOrDefault();

        //var activityModel = DataService._connection.Table<ActivityModel>();//.Where(
        //     x => x.BookId == 11);
        //&&
        //     x.Description == "book_test_1" &&
        //     x.Module == "WORD" &&
        //     x.Set == 0).FirstOrDefault();

        //select book
        //select activities in books where module
        List<BookGrade> bookGradeList = new List<BookGrade>();
        foreach (var b in book)
        {
            BookGrade _bf = new BookGrade(b);
            bookGradeList.Add(_bf);
            ModuleGrade wordGrade = new ModuleGrade();
            ModuleGrade observationGrade = new ModuleGrade();
            Debug.Log(b.Description);
            var activityModelWord = DataService._connection.Table<ActivityModel>().Where(x => x.BookId == b.Id && x.Module == "WORD");
            foreach (var act in activityModelWord)
            {
                Debug.Log(act.Description);
                var grades = DataService._connection.Table<StudentActivityModel>().Where(x => x.StudentId == 2 && x.SectionId == 1 && x.ActivityId == act.Id);
                foreach (var g in grades)
                {
                    wordGrade.Add(g.Grade);
                    Debug.Log(g.Grade);
                }
            }


            var activityModelObservation = DataService._connection.Table<ActivityModel>().Where(x => x.BookId == b.Id && x.Module == "OBSERVATION");
            foreach (var act in activityModelObservation)
            {
                Debug.Log(act.Description);
                var grades = DataService._connection.Table<StudentActivityModel>().Where(x => x.StudentId == 2 && x.SectionId == 1 && x.ActivityId == act.Id);
                foreach (var g in grades)
                {
                    observationGrade.Add(g.Grade);
                    Debug.Log(g.Grade);
                }
            }

            _bf.wordGrade = wordGrade;
            _bf.observationGrade = observationGrade;
            
        }
        Debug.Log("-----------------------------------------------");
        foreach (var bg in bookGradeList)
        {
            Debug.Log(bg.Description());
            Debug.Log(bg.wordGrade.GetAccuracy());
            Debug.Log(bg.observationGrade.GetAccuracy());
        }
    }

    class BookGrade
    {
        StudentModel student;
        BookModel book;
        public ModuleGrade wordGrade;
        public ModuleGrade observationGrade;

        public string Description()
        {
            return book.Description;
        }

        public BookGrade(BookModel book)
        {
            this.book = book;
        }
    }

    class ModuleGrade
    {
        List<string> lstGrade;
        int max= 0;
        public ModuleGrade()
        {
            lstGrade = new List<string>();
        }

        public void Add(string grade)
        {
            lstGrade.Add(grade);
        }

        public virtual double GetAccuracy()
        {
            double totalScore = 0;
            for (int i = 0; i < lstGrade.Count; i++)
            {
                if (lstGrade[i].Equals("A++"))
                {
                    totalScore += 100;
                }
                else if (lstGrade[i].Equals("A"))
                {
                    totalScore += 95;
                }
                else if (lstGrade[i].Equals("B+"))
                {
                    totalScore += 90;
                }
                else if (lstGrade[i].Equals("B"))
                {
                    totalScore += 85;
                }
                else if (lstGrade[i].Equals("C+"))
                {
                    totalScore += 80;
                }
                else if (lstGrade[i].Equals("C"))
                {
                    totalScore += 75;
                }
                else if (lstGrade[i].Equals("D+"))
                {
                    totalScore += 70;
                }
                else if (lstGrade[i].Equals("D"))
                {
                    totalScore += 65;
                }
                else if (lstGrade[i].Equals("E+"))
                {
                    totalScore += 60;
                }
                else if (lstGrade[i].Equals("E"))
                {
                    totalScore += 55;
                }
                else if (lstGrade[i].Equals("F"))
                {
                    totalScore += 50;
                }
                else
                {
                    totalScore += 100;
                }
            }
            //number of activities * 100
            max = lstGrade.Count * 100;
            
            return (totalScore / max) * 100;
        }

    }


}
#endif