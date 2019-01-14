using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using System;

public class CreateDBScript : MonoBehaviour
{
   [SerializeField]
   TimeUsageCounter timeUsageCounter;
   public Text DebugText;

   // Use this for initialization
   void Start()
   {

      //timeUsageCounter.Init(); //NOT A CREATE
      PlayerPrefs.SetInt ("subscriptionTime_table", 0);
      PlayerPrefs.SetInt ("adminDatabaseCreate", 0);

      if (0.Equals (PlayerPrefs.GetInt ("subscriptionTime_table")))
      {
         string path = Application.persistentDataPath + "/system";

         try
         {
            // Determine whether the directory exists.
            if (Directory.Exists (path))
            {
               return;
            }

            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory (path);
          

           
         }
         catch (Exception e)
         {
            
         }
         finally { }


         if (PlayerPrefs.GetInt ("adminDatabaseCreate").Equals (0))
         {
            DatabaseAdminController dac = new DatabaseAdminController ();
            dac.CreateAdminDb ();
            PlayerPrefs.SetInt ("adminDatabaseCreate", 1);

         }
         if (0.Equals (PlayerPrefs.GetInt ("subscriptionTime_table")))
         {
            DatabaseController dc = new DatabaseController ();
            dc.CreateSystemDB ("subscription.db");
            Debug.Log ("subs created");

            DataService.Open ("system/subscription.db");
            Debug.Log ("subs opened");

            DataService._connection.CreateTable<SubscriptionTimeModel> ();
            SubscriptionTimeModel model = new SubscriptionTimeModel
            {
               SettedTime = 1080000, //300hrs to seconds
               Timer = 1080000
            };
            DataService._connection.Insert (model);
            var subs = DataService.GetSubscription ();
            ToConsole (subs);

            DataService.Close ();
            PlayerPrefs.SetInt ("subscriptionTime_table", 1);

            timeUsageCounter.Init ();
         }
      }
      else
      {
         timeUsageCounter.Init ();
      }

   }

   IEnumerator IECreate(UnityAction[] actions, float[] time)
   {
      for (int i = 0; i < actions.Length; i++)
      {
         yield return new WaitForSeconds (time[i]);
         if (actions[i] != null)
         {
            Debug.Log (time[i]);
            actions[i] ();

         }

      }
   }

   private void ToConsole(IEnumerable<SubscriptionTimeModel> model)
   {
      foreach (var person in model)
      {
         Debug.Log (person.ToString ());
      }
   }

   private void ToConsole(string msg)
   {
      DebugText.text += System.Environment.NewLine + msg;
      Debug.Log (msg);
   }
}
