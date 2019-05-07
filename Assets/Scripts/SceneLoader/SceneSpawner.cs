using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneSpawner : MonoBehaviour
{
    public static SceneSpawner ins;
    public Transform parent;
    public Button UIBtnNext, UIBtnPrev;
    public List<GameObject> lstScenes = new List<GameObject>();
    
    List<GameObject> lstPool = new List<GameObject>();

    public GameObject curr, prev, next;
    
    GameObject sceneObject;
    
    [SerializeField]
    int sceneIndex = 0;
    
    SubtitleManager _subtitleManager;

    [SerializeField]
    bool isAssetBundle;
    [SerializeField]
    string assetBunldeUrlKey; //Beware of this. why? You'll find out soon.
    [SerializeField]
    string assetBundleUrlVersionKey;

    void Start()
    {
        _subtitleManager = GetComponent<SubtitleManager>();

        if (parent == null)
        {            
            if(GameObject.FindWithTag("Parent") == null)
                parent = GameObject.Find("Canvas_UI_SceneLoader").GetComponent<Transform>();
            else            
                parent = GameObject.FindWithTag("Parent").GetComponent<Transform>();
        }
                
        if (UIBtnNext == null)
        {
            UIBtnNext = GameObject.Find("_btnNext").GetComponent<Button>();
            UIBtnNext.onClick.AddListener(Next);
        }
        if (UIBtnPrev == null)
        {
            UIBtnPrev = GameObject.Find("_btnPrev").GetComponent<Button>();
            UIBtnPrev.onClick.AddListener(Prev);
        }
                
        StoryBookStart.instance.btnAgain.gameObject.SetActive(false);
        StoryBookStart.instance.btnAgain.onClick.AddListener(Replay);
        
        ins = this;
        sceneObject = Instantiate(lstScenes[sceneIndex]);       
        
        SetMyParent(sceneObject);       

        SetScale(sceneObject);
        
        lstPool.Add(sceneObject);
        curr = sceneObject;
        sceneIndex++; //for the "next" object see Line 26

        try
        {
            //pool the next object
            sceneObject = Instantiate(lstScenes[sceneIndex]);

            SetMyParent(sceneObject);
            
            SetScale(sceneObject);
            
            lstPool.Add(sceneObject);
            next = sceneObject;
            next.SetActive(false);
        }
        catch (System.ArgumentOutOfRangeException ex)
        {
            print(ex.Message.ToUpper());
        }
        curr.SetActive(true);
        _subtitleManager.ShowSubs(0);
        //play text animation

        Trace();
    }

    private void SetScale(GameObject sceneGameObject)
    {
        if(sceneGameObject.GetComponent<RectTransform>() != null)
            sceneGameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    void SetMyParent(GameObject _object)
    {
        _object.transform.SetParent(parent);        
        _object.transform.SetAsFirstSibling();
    }

    public void Prev()
    {
        if (UIBtnPrev.interactable)
        {

            if (prev != null)
            {
                sceneIndex--;
                curr = prev;
                try
                {
                    prev = lstPool[sceneIndex - 2];
                    next = lstPool[sceneIndex];
                    if (prev != null) { prev.SetActive(false); }
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                    prev = null;
                    next = lstPool[sceneIndex];
                    UIBtnPrev.interactable = false;
                }

                next.SetActive(false);
                if (curr != null)
                {
                    //print("MUST PLAY");
                    curr.SetActive(true); /* play text animation */ //curr.GetComponent<StoryBookPlayer>().PlayTextAnimation();
                    _subtitleManager.ShowSubs(sceneIndex - 1);
                }
                UI_SoundFX.ins.PlayUITurnPage();
                //print("pressed prev " + sceneIndex);
                //print("---prev---");
                Trace();
                UIBtnNext.interactable = true;
            }


        }

    }

    public void Next()
    {
        if (UIBtnNext.interactable)
        {
            prev = curr;
            curr = next;
            sceneIndex++; //for the "next" object
            if (curr == null)
            {
                print("END");
                //EmptySceneLoader.ins.sceneToLoad = SceneManager.GetActiveScene().name;
                //SceneLoader.instance.AsyncLoadStr("empty");
                if (isAssetBundle)
                {
                    string url = PlayerPrefs.GetString(assetBunldeUrlKey);
                    int version = PlayerPrefs.GetInt(assetBundleUrlVersionKey);

                    EmptySceneLoader.ins.loadUrl = url;
                    EmptySceneLoader.ins.loadVersion = version;
                    EmptySceneLoader.ins.sceneToLoad = StoryBookSaveManager.ins.GetBookScene();
                    EmptySceneLoader.ins.isAssetBundle = true;

                    //from itself
                    EmptySceneLoader.ins.unloadUrl = url;
                    EmptySceneLoader.ins.unloadVersion = version;
                    EmptySceneLoader.ins.unloadAll = false;

                    SceneManager.LoadSceneAsync("empty");
                    //try
                    //{

                    //    LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle(PlayerPrefs.GetString(assetBunldeUrlKey), PlayerPrefs.GetInt(assetBundleUrlVersionKey));
                    //    loader.OnLoadSceneFail += () =>
                    //    {
                    //        Debug.Log("Failed: Loading default");
                    //        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
                    //    };
                    //    loader.OnLoadSceneSuccess += () => { Debug.Log("Loading success!"); };
                    //    StartCoroutine(loader.IEStreamAssetBundle());
                    //}
                    //catch (System.Exception ex)
                    //{
                    //    Debug.LogError("The book url key downloaded from assetbundle not found.\n Download try downloading the book again from the launcher.");
                    //}
                }
                else
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
                }

            }
            else
            {
                try
                {
                    if (HasDuplicate(lstScenes[sceneIndex]))
                    {
                        next = lstPool[sceneIndex];
                    }
                    else if (!HasDuplicate(lstScenes[sceneIndex]))
                    {
                        sceneObject = Instantiate(lstScenes[sceneIndex]);

                        SetMyParent(sceneObject);
                        
                        SetScale(sceneObject);
                        
                        lstPool.Add(sceneObject);
                        next = sceneObject;
                    }
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                    next = null;
                }

                try
                {
                    if (curr != null)  /* play text animation */ //curr.GetComponent<StoryBookPlayer>().PlayTextAnimation();
                    {
                        curr.SetActive(true);
                        _subtitleManager.ShowSubs(sceneIndex - 1);
                    }

                    if (next != null) next.SetActive(false);
                    if (prev != null) prev.SetActive(false);
                    UI_SoundFX.ins.PlayUITurnPage();
                    //print("pressed next " + sceneIndex);
                    //print("---NEXT---");
                    Trace();
                }
                catch (System.NullReferenceException ex)
                {
                    print(ex);
                    //Application.LoadLevel(Application.loadedLevelName);
                }

                UIBtnPrev.interactable = true;

            }


        }

    }

    public void Replay()
    {
        _subtitleManager.ShowSubs(sceneIndex - 1);
    }

    void Trace()
    {
        if (prev != null) { }//print("prev " + prev.gameObject.name);
        if (curr != null) { }//print("curr " + curr.gameObject.name);
        if (next != null) { }//print("next " + next.gameObject.name);
    }

    bool HasDuplicate(GameObject obj)
    {
        string s;
        for (int i = 0; i < lstPool.Count; i++)
        {
            s = lstPool[i].gameObject.name.Replace("(Clone)", "");
            if (s == obj.name)
            {
                return true;
            }
        }

        return false;
    }

    public void EnableButtons()
    {
        UIBtnNext.gameObject.SetActive(true);
        UIBtnPrev.gameObject.SetActive(true);
        StoryBookStart.instance.btnAgain.gameObject.SetActive(true);
    }

    public void DisableButton()
    {
        UIBtnNext.gameObject.SetActive(false);
        UIBtnPrev.gameObject.SetActive(false);
        StoryBookStart.instance.btnAgain.gameObject.SetActive(false);
    }
}
