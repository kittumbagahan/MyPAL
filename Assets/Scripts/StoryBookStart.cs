using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryBookStart : MonoBehaviour
{

    public static StoryBookStart instance;
    [SerializeField]
    public GameObject book;
    [SerializeField]
    RectTransform btnNext, btnPrev, btnRead, btnAct, btnBookShelf, BG;
    [SerializeField]
    Button[] btns;
    public ReadType selectedReadType;
    [SerializeField]
    AudioClip audClipClick;
    AudioSource audSrc;
    CachedAssetBundle bundle;
    bool clicked;
    [SerializeField]
    bool downloadBook = true;
    [SerializeField]
    GameObject loadingObj;
	[SerializeField]
	float bgMusicVolume = 0.1f;
	//float tempBgMusicVolume;
    void Start()
    {
        instance = this;
        audSrc = GetComponent<AudioSource>();
        bundle = GetComponent<CachedAssetBundle>();
        btnNext.gameObject.SetActive(false);
        btnPrev.gameObject.SetActive(false);

		//reset the bg volume to original from reading volume 0.1f
		BG_Music.ins.SetVolume(0.5f);	
		//BG_Music.ins.SetToReadingVolume();
	}


    public void DisableButtons()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].interactable = false;
        }
    }


    public void Read(ReadType readType)
    {
        if (!clicked)
        {
            //DisableButtons();
            btnNext.gameObject.SetActive(true);
            btnPrev.gameObject.SetActive(true);
            clicked = true;
            selectedReadType = readType;
            audSrc.PlayOneShot(audClipClick);
            StartCoroutine(IERead());
            
            
            //btnNext.gameObject.SetActive(true);
            //btnPrev.gameObject.SetActive(true);
            //btnRead.gameObject.SetActive(false);
            //btnAct.gameObject.SetActive(false);
            //btnBookShelf.gameObject.SetActive(false);
            ////BG.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            //BG.gameObject.SetActive(false);
            ////transform.GetComponent<SceneSpawner>().enabled = true;
            //Instantiate(book, new Vector3(0, 0, 0), Quaternion.identity);
            //print(selectedReadType);
            //print(gameObject);


        }

    }
    IEnumerator IERead()
    {
		if(!BG_Music.ins.Audio.mute)
		{
			//print(BG_Music.ins.GetVolume() + " VOL");
			//BG_Music.ins.SetToReadingVolume();
		}
		BG_Music.ins.SetVolume(bgMusicVolume);
        if (!downloadBook)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(book, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else {
            loadingObj.SetActive(true);
            yield return StartCoroutine(bundle.IELoadAsset()); 
        }
       
        btnNext.gameObject.SetActive(true);
        btnPrev.gameObject.SetActive(true);
        loadingObj.SetActive(false);
        BG.gameObject.SetActive(false);
       
        

    }
    public void ReStart()
    {
        btnNext.gameObject.SetActive(false);
        btnPrev.gameObject.SetActive(false);
        //btnRead.gameObject.SetActive(true);
        //btnAct.gameObject.SetActive(true);
        //btnBookShelf.gameObject.SetActive(true);
        //BG.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
        BG.gameObject.SetActive(false);
        //transform.GetComponent<SceneSpawner>().enabled = false;
        print("Restart");
    }

    public void Activity(string name)
    {

        //Application.LoadLevel(name);
        audSrc.PlayOneShot(audClipClick);
        StartCoroutine(IELoad(name));
    }
    //float val = 0;
    IEnumerator IELoad(string s)
    {
        AsyncOperation async = null;
        async = Application.LoadLevelAsync(s);
        //val += 0.1f;
        //print(val);
        yield return async;

    }
    //public IEnumerator Load(string name)
    //{
    //    AsyncOperation async = null;
    //    if (name != null)
    //    {
    //        loading.gameObject.SetActive(true);
    //        async = Application.LoadLevelAsync(name);

    //    }
    //    yield return async;
    //}
}
