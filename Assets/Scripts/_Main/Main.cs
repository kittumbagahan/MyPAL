using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{				
		EmptySceneLoader.ins.sceneToLoad = "BookShelf";
		SceneManager.LoadScene("empty");
	}		
}
