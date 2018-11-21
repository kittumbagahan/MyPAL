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

            // network data
            NetworkData networkData = new NetworkData
            {
                ID = model.Id,
                sectionId = model.SectionId,
                studentId = model.StudentId,
                bookId = model.BookId,
                activityId = model.ActivityId,
                grade = model.Grade,
                playCount = model.PlayCount
            };

            ds._connection.Insert(model);

            // send data to server for insert
            MainNetwork.Instance.clientSendFile.SendData(networkData, ClientSendFile.MessageGroup.Insert);
        }
        else
        {
            print(grade + " updated!" );
            int playN = studentActivityModel.PlayCount + 1;

            string command = "Update StudentActivityModel set Grade='" + grade
                + "', PlayCount='" + playN + "' where Id='" + studentActivityModel.Id + "'";

            ds._connection.Execute(command);

            NetworkData networkData = new NetworkData
            {
                grade = grade,
                playCount = playN,
                ID = studentActivityModel.Id
            };

            MainNetwork.Instance.clientSendFile.SendData(networkData, ClientSendFile.MessageGroup.Update);
        }


        //PlayerPrefs.SetString("USERNAME" + storyBook.ToString() + module.ToString() + set, "done");
    }


}
