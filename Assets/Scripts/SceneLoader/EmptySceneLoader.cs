using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySceneLoader : MonoBehaviour {

	public static EmptySceneLoader ins;

	public string sceneToLoad;

    public bool isAssetBundle;

    public string unloadUrl;
    public int unloadVersion;
    public bool unloadAll;

    public string loadUrl;
    public int loadVersion;

	void Awake()
	{
		if(ins == null)
		{
			ins = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);

		}
	}

   
}
