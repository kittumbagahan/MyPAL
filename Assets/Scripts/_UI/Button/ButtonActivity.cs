using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class ButtonActivity : MonoBehaviour {

    [SerializeField]
    int index, buttonIndex;

    [SerializeField]
    SceneLoader sceneLoader;

    [SerializeField]
    Material grayscale;

    //[SerializeField]
    //Sprite done, notDone;

    [SerializeField]
    StoryBook storyBook;

    [SerializeField]
    Module module;    

	[SerializeField]
	string sceneToLoad;

	Animator animator;
    AudioSource audSrc;


    public String SceneToLoad {
        get { return sceneToLoad; }
    }

    public Module Mode {
        get { return module; }
    }

	void Start () {
		animator = GetComponent<Animator>();
        LoadStarButton();
        audSrc = GetComponent<AudioSource>();
	}

    public Material Grayscale { set { grayscale = value; } get { return grayscale; } }
    
    void LoadStarButton()
    {
		string saveState = "";
        try {
            //storyBook = Singleton.SelectedBook;
            storyBook = StoryBookSaveManager.ins.selectedBook;
            index = Read.instance.SceneIndex(storyBook, module, buttonIndex);
            sceneToLoad = Read.instance.SceneName(storyBook, module, buttonIndex);
			//2018 08 30//saveState = PlayerPrefs.GetString(StoryBookSaveManager.instance.oldUsername + storyBook.ToString() + sceneToLoad + module.ToString() + index);
			saveState = PlayerPrefs.GetString("section_id" + StoryBookSaveManager.ins.activeSection_id + "student_id" + StoryBookSaveManager.ins.activeUser_id +
                storyBook.ToString() + sceneToLoad + module.ToString() + index);
            //print(sceneToLoad + " on start " + gameObject.name);
			if (!saveState.Equals("0") && saveState != "")
            {
                /*change mat*/
                GetComponent<Image>().material = null;
            }
            else
            {
                /*change mat*/
                GetComponent<Image>().material = grayscale;
            }

            //print(StoryBookSaveManager.instance.oldUsername + storyBook + "_" + module + ", " + buttonIndex);
        }catch(Exception ex){
            //print("---------------------------------");
            //print(storyBook.ToString() + " " + index.ToString());
            //print(sceneToLoad + " wow");
            print(ex + "\n" + "THIS BUTTON HAS BEEN DISABLED. TRY REMOVING THE TRY CATCH BLOCK TO SEE WHY.");
            gameObject.SetActive(false);
        }
	
    }

    public void Click()
    {
        SaveTest.Set = index;
        SaveTest.module = module;
        SaveTest.storyBook = storyBook;
        //print("LOADING " + sceneToLoad);
		//BG_Music.ins.Mute();
		BG_Music.ins.SetToReadingVolume();
		StartCoroutine(IEClick());
        //Application.LoadLevel(sceneToLoad);
       
        /*
         the line below does not work
         */
		//SceneLoader.instance.LoadStr(sceneToLoad);
    }
    IEnumerator IEClick()
    {
      
        yield return new WaitForSeconds(1f);
        //SceneLoader.instance.LoadStr(sceneToLoad);
     
        //Application.LoadLevel(sceneToLoad);
        print(sceneToLoad + " LOAD THAT!");
        sceneLoader.AsyncLoadStr(sceneToLoad);
    }


	public void RandomShake()
	{
		animator.SetInteger("index", UnityEngine.Random.Range(0, 3));
	}
}
