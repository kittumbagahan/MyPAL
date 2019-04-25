using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectDatabase : UnityEditor.Editor {

	[MenuItem("Database/Open database")]
	public static void OpenDB()
	{
		DataService.Open("one.db");

		var all = DataService.GetActivities();

		foreach (var dat in all)
		{			
			Debug.Log(dat.ToString());
		}
	}
}
