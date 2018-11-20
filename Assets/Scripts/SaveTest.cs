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

        ActivityModel activityModel = ds._connection.Table<ActivityModel>().Where(x => x.BookId == ds._connection.Table<BookModel>()
            .Where(a => a.Description == storyBook.ToString()).FirstOrDefault().Id
            && x.Description == SceneManager.GetActiveScene().name
            && x.Module == module.ToString()
            && x.Set == set).FirstOrDefault();

        //    new ActivityModel
        //{
        //    BookId = ds._connection.Table<BookModel>().Where(x => x.Description == storyBook.ToString()).FirstOrDefault().Id,
        //    Description = SceneManager.GetActiveScene().name,
        //    Module = module.ToString(),
        //    Set = set
        //};

        StudentActivityModel studentActivityModel = ds._connection.Table<StudentActivityModel>().Where(x =>
            x.SectionId == StoryBookSaveManager.ins.activeSection_id &&
            x.StudentId == StoryBookSaveManager.ins.activeUser_id &&
            x.BookId == activityModel.BookId &&
            //ds._connection.Table<BookModel>().Where(a => a.Description == storyBook.ToString()).FirstOrDefault().Id &&
            x.ActivityId == activityModel.Id
            //ds._connection.Table<ActivityModel>().Where(b => b.BookId == activityModel.BookId && b.Description == activityModel.Description &&
            //   b.Module == activityModel.Module && b.Set == activityModel.Set).FirstOrDefault().Id
            ).FirstOrDefault();

        if (studentActivityModel == null)
        {
            StudentActivityModel model = new StudentActivityModel
            {
                SectionId = StoryBookSaveManager.ins.activeSection_id,
                StudentId = StoryBookSaveManager.ins.activeUser_id,
                BookId = activityModel.BookId,
                //ds._connection.Table<BookModel>().Where(x => x.Description == storyBook.ToString()).FirstOrDefault().Id,
                ActivityId = activityModel.Id,
                //ds._connection.Table<ActivityModel>().Where(x => x.BookId == activityModel.BookId &&
                //             x.Description == activityModel.Description &&
                //             x.Module == activityModel.Module && x.Set == activityModel.Set).FirstOrDefault().Id,
                Grade = ScoreManager.ins.GetGrade(),
                PlayCount = 1

            };
            ds._connection.Insert(model);
        }
        else
        {
            ds._connection.Execute("Update StudentActivityModel set Grade='" + ScoreManager.ins.GetGrade()
                + "', PlayCount='" + studentActivityModel.PlayCount+1 + "' where Id='" + activityModel.Id + "'");
        }


        //PlayerPrefs.SetString("USERNAME" + storyBook.ToString() + module.ToString() + set, "done");
    }


}
