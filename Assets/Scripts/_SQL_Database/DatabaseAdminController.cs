using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public sealed class DatabaseAdminController : DatabaseController
{
   
    public DatabaseAdminController() : base()
    {
        if (!File.Exists(DatabaseDirectory + "/" + "admin.db"))
        {
            File.Create(DatabaseDirectory + "/" + "admin.db").Close();

        }
        else
        {
            // throw new System.Exception();
        }


        
    }

    public void CreateAdminDb()
    {
        Debug.Log(Application.persistentDataPath);

        #region MAINTENANCE

       
        DataService.Open("admin.db");

        DataService._connection.CreateTable<AdminSectionsModel>();

        DataService._connection.CreateTable<ResetPasswordTimesModel>();
        ResetPasswordTimesModel resetPasswordTimesModel = new ResetPasswordTimesModel
        {
            MaxReset = 10,
            ResetCount = 0
        };
        DataService._connection.Insert(resetPasswordTimesModel);


        DataService._connection.CreateTable<NumberOfSectionsModel>();
        NumberOfSectionsModel numberOfSectionsModel = new NumberOfSectionsModel
        {
            MaxSection = 3
        };
        DataService._connection.Insert(numberOfSectionsModel);

        DataService._connection.CreateTable<NumberOfStudentsModel>();
        NumberOfStudentsModel numberOfStudentsModel = new NumberOfStudentsModel
        {
            MaxStudent = 250
        };
        DataService._connection.Insert(numberOfStudentsModel);


        DataService._connection.CreateTable<AdminPasswordModel>();
        AdminPasswordModel adminPasswordModel = new AdminPasswordModel
        {
            Password = "1234"
        };

        DataService._connection.Insert(adminPasswordModel);


        if ("".Equals(PlayerPrefs.GetString("deviceId_created")))
        {
            //do we need to save?
            PlayerPrefs.SetString("deviceId_created", SystemInfo.deviceUniqueIdentifier);
        }

        DataService._connection.CreateTable<TeacherDeviceModel>();

        DataService._connection.CreateTable<ResetPasswordModel>();
        DataService._connection.InsertAll(new[]{ new ResetPasswordModel
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

        DataService._connection.CreateTable<AdminPasswordModel>();

        DataService._connection.CreateTable<DeviceModel>();

        DataService.Close();
        #endregion MAINTENANCE

      
    }


}
