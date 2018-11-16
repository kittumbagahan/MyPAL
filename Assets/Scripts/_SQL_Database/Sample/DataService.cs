using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService
{

   public SQLiteConnection _connection { private set;  get; }

   public DataService(string DatabaseName)
   {

#if UNITY_EDITOR
      var dbPath = string.Format (@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
      _connection = new SQLiteConnection (dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
      Debug.Log ("Final PATH: " + dbPath);

   }


   public void CreateDB()
   {

      
      if (0.Equals (PlayerPrefs.GetInt ("section_table_created")))
      {
         _connection.CreateTable<SectionModel> ();

         PlayerPrefs.SetInt ("section_table_created", 1);
      }

      if (0.Equals (PlayerPrefs.GetInt ("student_table_created")))
      {
         _connection.CreateTable<StudentModel> ();
         PlayerPrefs.SetInt ("student_table_created", 1);
      }

      if (0.Equals (PlayerPrefs.GetInt ("book_table_created")))
      {
         _connection.CreateTable<BookModel> ();
      
         _connection.InsertAll (new[] {
                   new BookModel
                   {
                 
                      Description = StoryBook.ABC_CIRCUS.ToString()
                   },
                    new BookModel
                   {
                  
                      Description = StoryBook.AFTER_THE_RAIN.ToString()
                   },
                       new BookModel
                   {
                  
                      Description = StoryBook.CHAT_WITH_MY_CAT.ToString()
                   },
                  new BookModel
                   {
                    
                      Description = StoryBook.COLORS_ALL_MIXED_UP.ToString()
                   },
                     new BookModel
                   {
                   
                      Description = StoryBook.FAVORITE_BOX.ToString()
                   },
                     new BookModel
                   {
                    
                      Description = StoryBook.JOEY_GO_TO_SCHOOL.ToString()
                   },
                  new BookModel
                   {
                    
                      Description = StoryBook.SOUNDS_FANTASTIC.ToString()
                   },
                     new BookModel
                   {
                   
                      Description = StoryBook.TINA_AND_JUN.ToString()
                   },
                        new BookModel
                   {
                   
                      Description = StoryBook.WHAT_DID_YOU_SEE.ToString()
                   },
                           new BookModel
                   {
                      
                      Description = StoryBook.YUMMY_SHAPES.ToString()
                   }
             });

        
         PlayerPrefs.SetInt ("book_table_created", 1);
      }
      //MessageBox.ins.ShowOk (_connection.Table<BookModel> ().Where (x => x.Description == "ABC_CIRCUS").FirstOrDefault ().ToString (),
      //     MessageBox.MsgIcon.msgError, null);
      if (0.Equals (PlayerPrefs.GetInt ("activity_table_created")))
      {
         _connection.CreateTable<ActivityModel> ();
         //saved on ButtonActivity.cs
          
         PlayerPrefs.SetInt ("activity_table_created", 1);
      }

      //--------------------------------------------

      if (0.Equals (PlayerPrefs.GetInt ("studentActivityModel_table_created")))
      {
         _connection.CreateTable<StudentActivityModel> ();
         PlayerPrefs.SetInt ("studentActivityModel_table_created", 1);
      }

      if (0.Equals (PlayerPrefs.GetInt ("studentBookModel_table_created")))
      {
         _connection.CreateTable<StudentBookModel> ();
         PlayerPrefs.SetInt ("studentBookModel_table_created", 1);
      }



   }

   public IEnumerable<BookModel> GetBooks()
   {
      return _connection.Table<BookModel> ();
   }

   public IEnumerable<ActivityModel> GetActivities()
   {
      return _connection.Table<ActivityModel> ();
   }

   public IEnumerable<Person> GetPersonsNamedRoberto()
   {
      return _connection.Table<Person> ().Where (x => x.Name == "Roberto");
   }

   public Person GetJohnny()
   {
      return _connection.Table<Person> ().Where (x => x.Name == "Johnny").FirstOrDefault ();
   }

   public Person CreatePerson()
   {
      var p = new Person
      {
         Name = "Johnny",
         Surname = "Mnemonic",
         Age = 21
      };
      _connection.Insert (p);
      return p;
   }
}
