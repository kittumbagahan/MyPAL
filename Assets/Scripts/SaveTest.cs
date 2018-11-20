using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SaveTest : MonoBehaviour
{
    //SAVE ACTIVITY

    //these static variables are set on ButtonActivity.cs
    public static StoryBook storyBook;
    public static Module module;

    static int set;
    public static int Set
    {
        get { return set; }
        set { set = value; }
    }

    public static void Save()
    {
        //print("USERNAME"+storyBook.ToString() + ", " + module.ToString() +  ", " + Set);
        //-----------------------------------------------------------------------------------add scene name here
        PlayerPrefs.SetString("section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + StoryBookSaveManager.ins.activeUser_id
            + storyBook.ToString() + SceneManager.GetActiveScene().name + module.ToString() + set, ScoreManager.ins.GetGrade());

        print("SAVED " + ScoreManager.ins.GetGrade());
        print(StoryBookSaveManager.ins.Username + storyBook.ToString() + SceneManager.GetActiveScene().name + module.ToString() + set);
      
        DataService ds = new DataService("tempDatabase.db");

        print(storyBook.ToString());
        string bookname = storyBook.ToString();
        string modulename = module.ToString();
        string scenename = SceneManager.GetActiveScene().name;
        string grade = ScoreManager.ins.GetGrade();

        BookModel book = ds._connection.Table<BookModel>().Where(a => a.Description == bookname).FirstOrDefault();
       
        ActivityModel activityModel = ds._connection.Table<ActivityModel>().Where(
             x => x.BookId == book.Id &&
             x.Description == scenename &&
             x.Module == modulename &&
             x.Set == set).FirstOrDefault();

        StudentActivityModel studentActivityModel = ds._connection.Table<StudentActivityModel>().Where(x =>
            x.SectionId == StoryBookSaveManager.ins.activeSection_id &&
            x.StudentId == StoryBookSaveManager.ins.activeUser_id &&
            x.BookId == activityModel.BookId &&
            x.ActivityId == activityModel.Id
            ).FirstOrDefault();

        if (studentActivityModel == null)
        {
            StudentActivityModel model = new StudentActivityModel
            {
                Id =0,
                SectionId = StoryBookSaveManager.ins.activeSection_id,
                StudentId = StoryBookSaveManager.ins.activeUser_id,
                BookId = activityModel.BookId,
                ActivityId = activityModel.Id,
                Grade = grade,
                PlayCount = 1

            };
            ds._connection.Insert(model);
        }
        else
        {
            print(grade + " updated!" );
            int playN = studentActivityModel.PlayCount + 1;
            ds._connection.Execute("Update StudentActivityModel set Grade='" + grade
                + "', PlayCount='" + playN + "' where Id='" + studentActivityModel.Id + "'");
        }


        //PlayerPrefs.SetString("USERNAME" + storyBook.ToString() + module.ToString() + set, "done");
    }


}
