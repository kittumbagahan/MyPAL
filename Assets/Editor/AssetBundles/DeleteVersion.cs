using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeleteVersion : UnityEditor.Editor {

	[MenuItem("MyPAL/Delete App Version")]
	public static void DeleteAppVersion()
	{
		PlayerPrefs.DeleteKey("version");		
	}
}
