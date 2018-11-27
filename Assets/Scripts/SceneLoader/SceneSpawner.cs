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
    GameObject o;
    [SerializeField]
    int sceneIndex = 0;
    [SerializeField]
    float bookH = 1f, bookW = 1f;
    SubtitleManager subsMan;
    [SerializeField]
    

    void Start()
    {
        subsMan = GetComponent<SubtitleManager>();
        if (parent == null) parent = GameObject.Find("Canvas_UI_SceneLoader").GetComponent<Transform>();
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
        //print(lstScenes.Count - 1);
        ins = this;
        o = (GameObject)Instantiate(lstScenes[sceneIndex]);

        SetMyParent(o);

        lstPool.Add(o);
        curr = o;
        sceneIndex++; //for the "next" object see Line 26

        try
        {
            //pool the next object
            o = (GameObject)Instantiate(lstScenes[sceneIndex]);

            SetMyParent(o);

            lstPool.Add(o);
            next = o;
            next.SetActive(false);
        }
        catch (System.ArgumentOutOfRangeException ex)
        {
            print(ex.Message.ToUpper());
        }
        curr.SetActive(true);
        subsMan.ShowSubs(0);
        //play text animation
        
        //curr.GetComponent<StoryBookPlayer>().PlayTextAnimation();
        Trace();
    }

    void SetMyParent(GameObject _object)
    {
        _object.transform.SetParent(parent);
        //_object.transform.localPosition = new Vector3(-407, 304, 0);
        _object.transform.SetAsFirstSibling();
        //_object.transform.localPosition = new Vector3(0, 0, 0);
        //_object.transform.localScale = new Vector3(bookH, bookW, 1f);
    }

    public void Prev()
    {
        if (UIBtnPrev.interactable)
        {
            //sceneIndex--;
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
                    //next = lstPool[0];
                    //curr = next;
                    //print(ex.Message);
                    //sceneIndex++;
                    //print("dulo");
                    UIBtnPrev.interactable = false;
                }

                next.SetActive(false);
                if (curr != null)
                {
                    //print("MUST PLAY");
                    curr.SetActive(true); /* play text animation */ //curr.GetComponent<StoryBookPlayer>().PlayTextAnimation();
                    subsMan.ShowSubs(sceneIndex-1);
                }
                UI_SoundFX.ins.PlayUITurnPage();
                //print("pressed prev " + sceneIndex);
                //print("---prev---");
                Trace();
                UIBtnNext.interactable = true;
            }

            //try
            //{
            //    prev = lstPool[sceneIndex - 2];
            //    next = lstPool[sceneIndex];
            //    if (prev != null) { prev.SetActive(false); }
            //}
            //catch (System.ArgumentOutOfRangeException ex)
            //{
            //    prev = null;
            //    ////next = lstPool[sceneIndex];
            //    //next = lstPool[0];
            //    //curr = next;
            //    print(ex.Message);
            //    sceneIndex++;
            //    print("dulo");
            //    UIBtnPrev.interactable = false;
            //}

            //next.SetActive(false);
            //if (curr != null) curr.SetActive(true); /* play text animation */ curr.GetComponent<StoryBookPlayer>().PlayTextAnimation();
            //print("pressed prev " + sceneIndex);
            //print("---prev---");
            //Trace();
            //UIBtnNext.interactable = true;
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
                
                //Application.LoadLevel(Application.loadedLevelName);

				print("END");
				//CachedAssetBundle.ins.UnloadBundle();


				EmptySceneLoader.ins.sceneToLoad = SceneManager.GetActiveScene().name;
				SceneLoader.instance.AsyncLoadStr("empty");

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
                        o = (GameObject)Instantiate(lstScenes[sceneIndex]);

                        SetMyParent(o);

                        lstPool.Add(o);
                        next = o;
                    }
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                    //Application.LoadLevel(Application.loadedLevelName);
                    //print(ex);
                    next = null;
                    // UIBtnNext.interactable = false;
                }

                try
                {
                    if (curr != null)  /* play text animation */ //curr.GetComponent<StoryBookPlayer>().PlayTextAnimation();
                    {
                        curr.SetActive(true);
                        subsMan.ShowSubs(sceneIndex - 1);
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
                //if (curr != null) curr.SetActive(true); /* play text animation */ curr.GetComponent<StoryBookPlayer>().PlayTextAnimation();
                //if (next != null) next.SetActive(false);
                //if (prev != null) prev.SetActive(false);
                //print("pressed next " + sceneIndex);
                //print("---NEXT---");
                //Trace();

                UIBtnPrev.interactable = true;
            
            }
           
           
        }

    }

    void Trace()
    {
		if (prev != null) {}//print("prev " + prev.gameObject.name);
		if (curr != null) {}//print("curr " + curr.gameObject.name);
		if (next != null) {}//print("next " + next.gameObject.name);
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
    }

    public void DisableButton()
    {
        UIBtnNext.gameObject.SetActive(false);
        UIBtnPrev.gameObject.SetActive(false);
    }
}
