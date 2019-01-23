using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public Button btnAgain;
    void Start()
    {
        instance = this;
        audSrc = GetComponent<AudioSource>();
        bundle = GetComponent<CachedAssetBundle>();
        btnNext.gameObject.SetActive(false);
        btnPrev.gameObject.SetActive(false);
        btnAgain.gameObject.SetActive(false);


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


        }

    }
    IEnumerator IERead()
    {
        if (!BG_Music.ins.Audio.mute)
        {

        }
        BG_Music.ins.SetVolume(bgMusicVolume);
        if (!downloadBook)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(book, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
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

        BG.gameObject.SetActive(false);

        print("Restart");
    }

    public void Activity(string name)
    {
        
        try
        {

            LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle(PlayerPrefs.GetString("ActivitySelection_url_key"), PlayerPrefs.GetInt("ActivitySelection_version_key"));
            loader.OnLoadSceneFail += ()=>{ SceneManager.LoadSceneAsync(name); };
            loader.OnLoadSceneSuccess += Success;
            StartCoroutine(loader.IEStreamAssetBundle());
        }
        catch (LoadSceneFromAssetBundleException ex)
        {
            Debug.LogError("The book url key downloaded from assetbundle not found.\n Download try downloading the book again from the launcher.");
        }
        //Application.LoadLevel(name);
        audSrc.PlayOneShot(audClipClick);
        

    }

    void Success()
    {
        Debug.Log("Loading scene success");
    }

}
