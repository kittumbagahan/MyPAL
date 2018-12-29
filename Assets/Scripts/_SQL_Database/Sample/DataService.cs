using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

using System.Threading;

public static class DataService
{

    public static SQLiteConnection _connection { private set; get; }

    //public DataService()
    //{        
    //       string DatabaseName; /* = PlayerPrefs.GetString("activeDatabase") == "" ? "tempDatabase.db" : PlayerPrefs.GetString("activeDatabase");*/
    //         DatabaseName = DbName ();
    //       string dbPath = Application.persistentDataPath + "/" + DatabaseName;
    //   Debug.Log (dbPath);
    //   //#if UNITY_EDITOR
    //   //        //var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
    //   //#else
    //   //        // check if file exists in Application.persistentDataPath
    //   //        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

    //   //        if (!File.Exists(filepath))
    //   //        {
    //   //            Debug.Log("Database not in Persistent path");
    //   //            // if it doesn't ->
    //   //            // open StreamingAssets directory and load the db ->

    //   //#if UNITY_ANDROID
    //   //            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
    //   //            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
    //   //            // then save to Application.persistentDataPath
    //   //            File.WriteAllBytes(filepath, loadDb.bytes);
    //   //#elif UNITY_IOS
    //   //                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
    //   //                // then save to Application.persistentDataPath
    //   //                File.Copy(loadDb, filepath);
    //   //#elif UNITY_WP8
    //   //                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
    //   //                // then save to Application.persistentDataPath
    //   //                File.Copy(loadDb, filepath);

    //   //#elif UNITY_WINRT
    //   //		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
    //   //		// then save to Application.persistentDataPath
    //   //		File.Copy(loadDb, filepath);

    //   //#elif UNITY_STANDALONE_OSX
    //   //		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
    //   //		// then save to Application.persistentDataPath
    //   //		File.Copy(loadDb, filepath);
    //   //#else
    //   //	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
    //   //	// then save to Application.persistentDataPath
    //   //	File.Copy(loadDb, filepath);

    //   //#endif

    //   //            Debug.Log("Database written");
    //   //        }

    //   //        var dbPath = filepath;
    //   //#endif
    //       _connection = new SQLiteConnection (dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    //       Debug.Log ("Final PATH: " + dbPath);        
    //}    

    public static void Open()
    {
        string DatabaseName; /* = PlayerPrefs.GetString("activeDatabase") == "" ? "tempDatabase.db" : PlayerPrefs.GetString("activeDatabase");*/
        DatabaseName = DbName();
        string dbPath = Application.persistentDataPath + "/" + DatabaseName;

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        //Debug.Log("Open Final PATH: " + dbPath);
    }

    public static void Open(string DatabaseName)
    {
        string dbPath = Application.persistentDataPath + "/" + DatabaseName;
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Open Final PATH: " + dbPath);
    }

    public static void Close()
    {
        _connection.Close();
        _connection.Dispose();
        GC.Collect();
        //Debug.Log("Close connection");
    }

    public static string DbName()
    {
        if (PlayerPrefs.GetString("activeDatabase") == "")
        {
            PlayerPrefs.SetString("activeDatabase", "tempDatabase.db");
            return "tempDatabase.db";
        }
        else
        {
            Debug.Log("HOY! " + PlayerPrefs.GetString("activeDatabase"));
            return PlayerPrefs.GetString("activeDatabase");
        }
    }

    public static void SetDbName(string pDbName)
    {
        PlayerPrefs.SetString("activeDatabase", pDbName);
    }

    // public DataService(string DatabaseName)
    // {
    //     string dbPath = Application.persistentDataPath + "/" + DatabaseName;
    //   //#if UNITY_EDITOR
    //   //      var dbPath = string.Format (@"Assets/StreamingAssets/{0}", DatabaseName);
    //   //#else
    //   //        // check if file exists in Application.persistentDataPath
    //   //        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

    //   //        if (!File.Exists(filepath))
    //   //        {
    //   //            Debug.Log("Database not in Persistent path");
    //   //            // if it doesn't ->
    //   //            // open StreamingAssets directory and load the db ->

    //   //#if UNITY_ANDROID
    //   //            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
    //   //            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
    //   //            // then save to Application.persistentDataPath
    //   //            File.WriteAllBytes(filepath, loadDb.bytes);
    //   //#elif UNITY_IOS
    //   //                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
    //   //                // then save to Application.persistentDataPath
    //   //                File.Copy(loadDb, filepath);
    //   //#elif UNITY_WP8
    //   //                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
    //   //                // then save to Application.persistentDataPath
    //   //                File.Copy(loadDb, filepath);

    //   //#elif UNITY_WINRT
    //   //		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
    //   //		// then save to Application.persistentDataPath
    //   //		File.Copy(loadDb, filepath);

    //   //#elif UNITY_STANDALONE_OSX
    //   //		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
    //   //		// then save to Application.persistentDataPath
    //   //		File.Copy(loadDb, filepath);
    //   //#else
    //   //	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
    //   //	// then save to Application.persistentDataPath
    //   //	File.Copy(loadDb, filepath);

    //   //#endif

    //   //            Debug.Log("Database written");
    //   //        }

    //   //        var dbPath = filepath;
    //   //#endif
    //       _connection = new SQLiteConnection (dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    //       Debug.Log ("Final PATH: " + dbPath);

    //}

    public static void CreateSubscriptionDB()
    {
        if (0.Equals(PlayerPrefs.GetInt("subscriptionTime_table")))
        {
            _connection.CreateTable<SubscriptionTimeModel>();
            SubscriptionTimeModel model = new SubscriptionTimeModel
            {
                SettedTime = 1080000, //300hrs to seconds
                Timer = 1080000
            };
            _connection.Insert(model);
            PlayerPrefs.SetInt("subscriptionTime_table", 1);
        }
    }

    public static void CreateDB()
    {
        Debug.Log(Application.persistentDataPath);

        #region MAINTENANCE

        if (0.Equals(PlayerPrefs.GetInt("resetPasswordTimes_table")))
        {
            _connection.CreateTable<ResetPasswordTimesModel>();
            ResetPasswordTimesModel model = new ResetPasswordTimesModel
            {
                MaxReset = 10,
                ResetCount = 0
            };
            _connection.Insert(model);
            PlayerPrefs.SetInt("resetPasswordTimes_table", 1);
        }

        if (0.Equals(PlayerPrefs.GetInt("numberOfStudents_table")))
        {
            _connection.CreateTable<NumberOfStudentsModel>();
            NumberOfStudentsModel model = new NumberOfStudentsModel
            {
                MaxStudent = 250
            };
            _connection.Insert(model);
            PlayerPrefs.SetInt("numberOfStudents_table", 1);
        }

        if (0.Equals(PlayerPrefs.GetInt("adminPassword_table")))
        {
            _connection.CreateTable<AdminPasswordModel>();
            AdminPasswordModel model = new AdminPasswordModel
            {
                Password = "1234"
            };

            _connection.Insert(model);
            PlayerPrefs.SetInt("adminPassword_table", 1);
        }

        if ("".Equals(PlayerPrefs.GetString("deviceId_created")))
        {
            //do we need to save?
            PlayerPrefs.SetString("deviceId_created", SystemInfo.deviceUniqueIdentifier);
        }

        if (0.Equals(PlayerPrefs.GetInt("teacherDevice_table_created")))
        {
            _connection.CreateTable<TeacherDeviceModel>();
            PlayerPrefs.SetInt("teacherDevice_table_created", 1);
        }

        if (0.Equals(PlayerPrefs.GetInt("resetPassword_table_created")))
        {
            _connection.CreateTable<ResetPasswordModel>();
            _connection.InsertAll(new[]{ new ResetPasswordModel
            {
                SystemPasscode = "0AAA",
                Used = false
            },
            new ResetPasswordModel
            {
                 SystemPasscode = "1BBB",
                Used = false
            },
            new ResetPasswordModel
            {
                 SystemPasscode = "2CCC",
                Used = false
            },
            new ResetPasswordModel
            {
                 SystemPasscode = "3DDD",
                Used = false
            },
            new ResetPasswordModel
            {
                 SystemPasscode = "4EEE",
                Used = false
            },
            new ResetPasswordModel
            {
                 SystemPasscode = "5FFF",
                Used = false
            },
            new ResetPasswordModel
            {
                 SystemPasscode = "6GGG",
                Used = false
            },
            new ResetPasswordModel
            {
                 SystemPasscode = "7HHH",
                Used = false
            },
            new ResetPasswordModel
            {
                 SystemPasscode = "8III",
                Used = false
            },
            new ResetPasswordModel
            {
                 SystemPasscode = "9JJJ",
                Used = false
            },
            });
            PlayerPrefs.SetInt("resetPassword_table_created", 1);
        }
        if (0.Equals(PlayerPrefs.GetInt("adminPassword_table_created")))
        {
            _connection.CreateTable<AdminPasswordModel>();
            AdminPasswordModel model = new AdminPasswordModel
            {
                Password = "1234"
            };
            _connection.Insert(model);
            PlayerPrefs.SetInt("adminPassword_table_created", 1);
        }
    
        if (0.Equals(PlayerPrefs.GetInt("device_table_created")))
        {
            _connection.CreateTable<DeviceModel>();
            //add UID through networking
            PlayerPrefs.SetInt("device_table_created", 1);
        }
        #endregion MAINTENANCE
    }

    public static IEnumerable<SubscriptionTimeModel> GetSubscription()
    {
        return _connection.Table<SubscriptionTimeModel>();
    }

    public static IEnumerable<BookModel> GetBooks()
    {
        return _connection.Table<BookModel>();
    }

    public static IEnumerable<ActivityModel> GetActivities()
    {
        return _connection.Table<ActivityModel>();
    }

    public static IEnumerable<SectionModel> GetSections()
    {
        return _connection.Table<SectionModel>();
    }

    public static IEnumerable<StudentModel> GetStudents()
    {
        return _connection.Table<StudentModel>();
    }

    public static IEnumerable<StudentActivityModel> GetStudentActivities()
    {
        return _connection.Table<StudentActivityModel>();
    }

    public static IEnumerable<StudentBookModel> GetStudentBooks()
    {
        return _connection.Table<StudentBookModel>();
    }


    public static Person CreatePerson()
    {
        var p = new Person
        {
            Name = "Johnny",
            Surname = "Mnemonic",
            Age = 21
        };
        _connection.Insert(p);
        return p;
    }
}
