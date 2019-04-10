using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using _Assetbundle;

public class ActivityDone : MonoBehaviour {

	public static ActivityDone instance;
    //[SerializeField]
    //bool showAds;
	[SerializeField]
	AudioClip[] goodJob;
    [SerializeField]
    AudioClip doneFX;
	AudioSource audioSource;
	
	[SerializeField]
	RectTransform dialog;

	//[SerializeField]
	string levelToLoad = "ActivitySelection";

	// Use this for initialization
	void Start () {
		instance = this;
		audioSource = GetComponent<AudioSource>();
	}

    void Update()
    { 
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Item.RemoveSubscribers();
            WordGameManager.RemoveSubscribers();
            WordGameManager_2.RemoveSubscribers();
                       
            SceneManager.LoadScene(0);
        }
    }

	public void Done()
	{
        //if (showAds) AdsManager.ins.ShowAds();
        audioSource.clip = doneFX;
        audioSource.Play();
        Item.RemoveSubscribers();
        WordGameManager.RemoveSubscribers();
        WordGameManager_2.RemoveSubscribers();
		StartCoroutine(ShowDialog());
	}

	IEnumerator ShowDialog()
	{
		dialog.gameObject.SetActive(true);//Show Dialog

		audioSource.PlayOneShot(goodJob[Random.Range(0, goodJob.Length)]);

		yield return new WaitForSeconds(3);
     
        if (1 == UserRestrictionController.ins.restriction)
        {
            SaveTest.Save();
            ToActivitySelection();
        }
        else {
            ToActivitySelection();
           // MessageBox.ins.ShowOk("This is not saved.", MessageBox.MsgIcon.msgInformation, new UnityAction(ToActivitySelection));
        }
       
		//Application.LoadLevel(levelToLoad);
      
	}

    void ToActivitySelection()
    {
        string url = PlayerPrefs.GetString("ActivitySelection_url_key");
        int version = PlayerPrefs.GetInt("ActivitySelection_version_key");

        //this is from activity so
        EmptySceneLoader.ins.unloadUrl = PlayerPrefs.GetString(AssetBundleInfo.ActivityScene.urlKey);
        EmptySceneLoader.ins.unloadVersion = PlayerPrefs.GetInt(AssetBundleInfo.ActivityScene.versionKey);
        EmptySceneLoader.ins.unloadAll = false;

        if (!"".Equals(url))
        {
            EmptySceneLoader.ins.loadUrl = url;
            EmptySceneLoader.ins.loadVersion = version;
            EmptySceneLoader.ins.sceneToLoad = levelToLoad;
            EmptySceneLoader.ins.isAssetBundle = true;

            //unload activity
            if (AssetBundleInfo.ActivityScene.isAssetBundle)
            {
                Debug.Log("Loaded from assetbundle activity");
          
                SceneManager.LoadSceneAsync("empty");
               
            }
            //nothing to unload
            else
            {
                EmptySceneLoader.ins.unloadUrl = "";
                EmptySceneLoader.ins.unloadVersion = 0;
                EmptySceneLoader.ins.unloadAll = false;

                //clear activity assetbundle info
                AssetBundleInfo.ActivityScene.urlKey = "";
                AssetBundleInfo.ActivityScene.versionKey = "";
                AssetBundleInfo.ActivityScene.isAssetBundle = false;

                Debug.Log("Loaded from default activity");

                SceneManager.LoadSceneAsync("empty");
            }
        }
        //if launcher is not from assetbundle
        //but activity maybe an assetbundle
        else
        {
            EmptySceneLoader.ins.isAssetBundle = false;
            EmptySceneLoader.ins.sceneToLoad = levelToLoad;
            EmptySceneLoader.ins.loadUrl = "";
            EmptySceneLoader.ins.loadVersion = 0;

            //clear activity assetbundle info
            AssetBundleInfo.ActivityScene.urlKey = "";
            AssetBundleInfo.ActivityScene.versionKey = "";
            AssetBundleInfo.ActivityScene.isAssetBundle = false;

            Debug.Log("Loaded from default activity 2");

            SceneManager.LoadSceneAsync("empty");
        }


       
       

       
    }

	public void PlayAgain()
	{
        //SceneManager.LoadScene(SceneManager.GetActiveScene);
	
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
