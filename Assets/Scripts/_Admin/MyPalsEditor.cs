using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

#if UNITY_EDITOR 
using UnityEditor;
public class MyPalsEditor {

    [MenuItem("Assets/Reset MyPals data")]
    static void ResetMyPalsData()
    {
      
        string[] dbFiles = Directory.GetFiles(Application.persistentDataPath, "*.db");
      
        PlayerPrefs.SetInt("subscriptionTime_table", 0);
        PlayerPrefs.SetInt("adminDatabaseCreate", 0);



        try
        {
            foreach(string db in dbFiles)
            {
                File.Delete(db);
            }
            Directory.Delete(Application.persistentDataPath + "/system", true);
         
        }
        catch (DirectoryNotFoundException ex)
        {
            Debug.LogError(ex.Message);
        }
    }

}
#endif