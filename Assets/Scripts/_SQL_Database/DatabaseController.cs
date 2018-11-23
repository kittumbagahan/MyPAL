using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DatabaseController : MonoBehaviour {

	// Use this for initialization
	void Start () {
      CopySomething ();
	}

   public void CopySomething()
   {
      //FileUtil.CopyFileOrDirectory ("sourcepath/YourFileOrFolder", "destpath/YourFileOrFolder");
      FileUtil.CopyFileOrDirectory ("Assets/StreamingAssets/tempDatabase.db", "Assets/StreamingAssets/copy_tempDatabase.db");
   }
}
