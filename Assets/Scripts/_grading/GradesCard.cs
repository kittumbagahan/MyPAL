using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using UnityEngine.UI;

using System.IO;
using System.Text;
using System;
using System.Linq;

using UnityEngine.SceneManagement;

public class GradesCard : MonoBehaviour
{

    [SerializeField]
    List<StudentGradeCard> sgc;
    [SerializeField]
    GameObject txtprefab;
    [SerializeField]
    Transform parent;

    [SerializeField]
    Button btnExportData;
    [SerializeField]
    Text txtData;
    // Use this for initialization
    [SerializeField]
    GameObject loadingPanel;


    // data export
    protected string columns = "Fullname,Word,Observation,Total";
    protected string data;
    protected string textExport;


    StringBuilder SetStringLen(string s, int len = 50)
    {
        StringBuilder sb = new StringBuilder(s);
        int strLen = sb.Length;
        while (strLen++ < len)
        {
            sb.Append(" ");
        }

        return sb;
    }

    IEnumerator IEComputeGrades()
    {
        float progress = 0;
        float counter = 0;
        Text txtLoading = loadingPanel.GetComponentInChildren<Text>();
        loadingPanel.gameObject.SetActive(true);
        DataService.Open();

        var students = DataService._connection.Table<StudentModel>().Where(x => x.SectionId == StoryBookSaveManager.ins.activeSection_id).OrderBy(x => x.Gender);
        var book = DataService._connection.Table<BookModel>();

        txtData.text = "<b>" + SetStringLen("Fullname,", 55) + SetStringLen("Word,", 15) + SetStringLen("Observation,", 20) + SetStringLen("Total,", 10) + "</b>\n";

        foreach (StudentModel s in students)
        {
            List<BookGrade> bookGradeList = new List<BookGrade>();

            foreach (var b in book)
            {
                BookGrade _bf = new BookGrade(b, s);
                bookGradeList.Add(_bf);
                ModuleGrade wordGrade = new ModuleGrade();
                ModuleGrade observationGrade = new ModuleGrade();
                //Debug.Log(b.Description);
                var activityModelWord = DataService._connection.Table<ActivityModel>().Where(x => x.BookId == b.Id && x.Module == "WORD");
                foreach (var act in activityModelWord)
                {
                    //Debug.Log(act.Description);
                    var grades = DataService._connection.Table<StudentActivityModel>().Where(x => x.StudentId == s.Id && x.SectionId == s.SectionId && x.ActivityId == act.Id);
                    foreach (var g in grades)
                    {
                        wordGrade.Add(g.Grade);
                        //Debug.Log(g.Grade);
                    }
                }

                var activityModelObservation = DataService._connection.Table<ActivityModel>().Where(x => x.BookId == b.Id && x.Module == "OBSERVATION");
                foreach (var act in activityModelObservation)
                {
                    //Debug.Log(act.Description);
                    var grades = DataService._connection.Table<StudentActivityModel>().Where(x => x.StudentId == s.Id && x.SectionId == s.SectionId && x.ActivityId == act.Id);
                    foreach (var g in grades)
                    {
                        observationGrade.Add(g.Grade);
                        //Debug.Log(g.Grade);
                    }
                }

                _bf.wordGrade = wordGrade;
                _bf.observationGrade = observationGrade;

            }

            double wordTotalGrade = 0;// = bookGradeList.Sum(x => x.wordGrade.GetAccuracy());
            double observationTotalGrade = 0;// = bookGradeList.Sum(x => x.observationGrade.GetAccuracy());

            foreach (var bg in bookGradeList)
            {
                wordTotalGrade += bg.wordGrade.GetAccuracy();
                observationTotalGrade += bg.observationGrade.GetAccuracy();
            }
            Debug.Log("Wordy!" + wordTotalGrade);

            if (s.Gender.Equals("Male")) txtData.text += "<color=#0000a0ff>";
            else txtData.text += "<color=#ff00ffff>";
            if (IsIncomplete(bookGradeList))
            {

                txtData.text += SetStringLen("\n" + s.Lastname + " " + s.Givenname + " " + s.Middlename + ", ",50).ToString() +
             SetStringLen(string.Format("{0:0.00}", wordTotalGrade) + " inc" + ",", 20) + SetStringLen(string.Format("{0:0.00}", observationTotalGrade) + " inc" + ",", 20) + SetStringLen(string.Format("{0:0.00}", ((wordTotalGrade + observationTotalGrade) / 2)) + " inc", 10);
            }
            else
            {
                txtData.text += SetStringLen("\n" + s.Lastname + " " + s.Givenname + " " + s.Middlename + ", ", 50).ToString() +
             SetStringLen(string.Format("{0:0.00}", wordTotalGrade) + ",", 20) + SetStringLen(string.Format("{0:0.00}", observationTotalGrade) + ",", 20) + SetStringLen(string.Format("{0:0.00}", ((wordTotalGrade + observationTotalGrade) / 2)), 10);
            }
            txtData.text += "</color>";

            data += string.Format("\"{0}, {1} {2}.\"", s.Lastname, s.Givenname, s.Middlename) + "," + wordTotalGrade + "," + observationTotalGrade +
                "," + (wordTotalGrade + observationTotalGrade) + Environment.NewLine;
            counter++;
            progress = (counter / (float)students.Count()) * 100;
            txtLoading.text = "Loading " + progress.ToString() + "%";
            yield return null;
        }
        loadingPanel.gameObject.SetActive(false);
        DataService.Close();
    }

    private void OnEnable()
    {
        // data        
        columns += Environment.NewLine + "," + Environment.NewLine;

        StartCoroutine(IEComputeGrades());

    }


    bool IsIncomplete(List<BookGrade> bg)
    {
      
        foreach (BookGrade g in bg)
        {
            if (g.wordGrade.GetAccuracy() == 0 || g.observationGrade.GetAccuracy() == 0)
            {
                return true;
            }
        }
        return false;
    }

    // test
    #region DATA
    public void ExportData()
    {
        Debug.Log("Loading from GradesCard " + gameObject.name);
        SceneManager.LoadScene("DataImporter");
        //File.WriteAllText(Application.persistentDataPath + "/studentData.csv", textExport);
        //Debug.Log("Check File at " + Application.persistentDataPath);
        //MessageBox.ins.ShowOk("Data export successful!", MessageBox.MsgIcon.msgInformation, null);
    }
    #endregion


    class BookGrade
    {
        public StudentModel student;
        BookModel book;
        public ModuleGrade wordGrade;
        public ModuleGrade observationGrade;

        public string Description()
        {
            return book.Description;
        }

        public BookGrade(BookModel book, StudentModel student)
        {
            this.book = book;
            this.student = student;
        }
    }

    class ModuleGrade
    {
        List<string> lstGrade;
        int max = 0;
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

            double res = (totalScore / (double)max) * 100;
            if (res.HasValue())
            {
                return res;
            }
            //Debug.Log("Res " + res);

            return 0;
        }

    }
}
