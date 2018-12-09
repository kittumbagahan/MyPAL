using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class CreateDBScript : MonoBehaviour
{

   public Text DebugText;

   // Use this for initialization
   void Start()
   {

      StartSync ();


   }

   private void StartSync()
   {
      //set the active database for the app for the firstime
      //if ("".Equals(PlayerPrefs.GetString("activeDatabase")))
      //{
      //    PlayerPrefs.SetString("activeDatabase", "tempDatabase.db");
      //}
     
      var ds = new DataService ();
      ds.CreateDB ();
      var books = ds.GetBooks ();
      var acts = ds.GetActivities ();
      var sections = ds.GetSections ();
      var st = ds.GetStudents ();
      var studentActivities = ds.GetStudentActivities ();
      var studentBooks = ds.GetStudentBooks ();

      DatabaseController dc = new DatabaseController ();
      dc.CreateDB ("subscription.db");
      var ds2 = new DataService ("system/subscription.db");
      ds2.CreateSubscriptionDB ();
      var subs = ds2.GetSubscription ();
      ToConsole (subs);
   }

   private void ToConsole(IEnumerable<SubscriptionTimeModel> model)
   {
      foreach (var person in model)
      {

         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }

   private void ToConsole(IEnumerable<BookModel> model)
   {
      foreach (var person in model)
      {

         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }
   private void ToConsole(IEnumerable<ActivityModel> model)
   {
      foreach (var person in model)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }
   private void ToConsole(IEnumerable<SectionModel> model)
   {
      foreach (var person in model)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }

   private void ToConsole(IEnumerable<StudentModel> model)
   {
      foreach (var person in model)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }

   private void ToConsole(IEnumerable<StudentActivityModel> model)
   {
      foreach (var person in model)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }
   private void ToConsole(IEnumerable<StudentBookModel> model)
   {
      foreach (var person in model)
      {
         //ToConsole(person.ToString());
         Debug.Log (person.ToString ());
      }
   }

   private void ToConsole(string msg)
   {
      DebugText.text += System.Environment.NewLine + msg;
      Debug.Log (msg);
   }
}
