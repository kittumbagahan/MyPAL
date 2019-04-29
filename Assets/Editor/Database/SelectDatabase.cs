using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectDatabase : UnityEditor.Editor {

	[MenuItem("Database/Select Books per section")]
	public static void OpenDB()
	{
		DataService.Open("one.db");

		var all = DataService.GetActivities();		
		
		foreach (var dat in all)
		{			
			Debug.Log(dat.ToString());
		}
		DataService.Close();
	}

	[MenuItem("Database/Select all sections")]
	public static void SelecAllSections()
	{
		DataService.Open("system/admin.db");

		var sections = DataService._connection.Table<AdminSectionsModel>();

		foreach (var section in sections)
		{
			Debug.Log(section.Description);
		}
		
		DataService.Close();
	}
}
