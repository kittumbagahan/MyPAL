using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarouItem : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    string bundlePID;
    [SerializeField]
    byte bookIndex;

    public Transform destination;
    private Transform parentContent;

    float dist = 0;

    public float min, max;
    [SerializeField]
    string assetBunldeUrlKey; //Beware of this. why? You'll find out soon.
    [SerializeField]
    string assetBundleUrlVersionKey;
    [SerializeField]
    bool isAssetBundle;
    [SerializeField]
    string sceneToLoad;
    public bool isClickable;
    bool clicked;
    [SerializeField]
    StoryBook selectedStoryBook;
    private bool spotLight;
    private float origScale = 1;
    [SerializeField]
    GameObject /*imgHighlight,*/ imgClickEffect;
    bool highlighted = false;
    bool highlightPlayed = false;
    //[SerializeField]
    //int locked = 0;
    Image _img;

    void Start()
    {
        parentContent = transform.parent;
        origScale -= 0.1f;
        _img = GetComponent<Image>();

    }

    void Update()
    {
        dist = Vector2.Distance(transform.position, destination.position);
        //print(dist);
        if (dist >= min && dist <= max)
        {


            //isClickable = true;
            if (!BookshelfManager.ins.aBookIsActive)
            {
                transform.SetAsLastSibling();
                Grow();
                BookshelfManager.ins.aBookIsActive = true;
            }

            if (!BookshelfManager.ins.AudioSrc.isPlaying && isClickable == false && !highlightPlayed)
            {
                if (!clicked)
                {
                    //transform.parent.GetComponent<AudioSource>().PlayOneShot(BookshelfManager.ins.AudClipBookHighlight); 
                    BookshelfManager.ins.PlayBookActive();
                    highlightPlayed = true;
                    isClickable = true;

                }

            }

            Book.instance.BookCover(bookIndex);
            Book.instance.BookDescription(bookIndex);
        }
        else
        {
            //transform.SetAsFirstSibling();
            if (isClickable && BookshelfManager.ins.aBookIsActive)
                BookshelfManager.ins.aBookIsActive = false;
            isClickable = false;
            highlightPlayed = false;
            Shrink();
        }

    }

    void Grow()
    {
        if (transform.GetWidth() < 320f)
        {
            transform.SetHeight(Mathf.Lerp(transform.GetHeight(), 275f, 0.5f));
            transform.SetWidth(Mathf.Lerp(transform.GetWidth(), 290f, 0.5f));
        }
    }

    void Shrink()
    {
        if (transform.GetWidth() > 160f)
        {
            // print(transform.localScale.x);
            transform.SetHeight(Mathf.Lerp(transform.GetHeight(), 150f, 0.08f));
            transform.SetWidth(Mathf.Lerp(transform.GetWidth(), 160f, 0.08f));

        }
        if (transform.localScale.x < 1.1f)
        {
            //  transform.SetLocalWidth(1f);
            //transform.SetLocalHeight(1f);
        }

    }

    public void Click()
    {
        //Application.LoadLevel(sceneToLoad);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isClickable)
        {
            if (sceneToLoad == "")// && locked == 1)
            {
                MessageBox.ins.ShowOk("SceneToLoad empty!", MessageBox.MsgIcon.msgError, null);
                return;
            }

            try
            {
                if (TimeUsageCounter.ins == null)
                    throw new TimeUsageException("TimeUsageCounter is not initialized.", TimeUsageException.ErrorCode.NullReference);
                if (TimeUsageCounter.ins.IsTimeOver())
                {
                    MessageBox.ins.ShowOk("Subscription expired. \nPlease email us at \npalabaydev@gmail.com", MessageBox.MsgIcon.msgError, null);
                }
                else
                {
                    //print("print " + sceneToLoad);
                    StoryBookSaveManager.ins.selectedBook = selectedStoryBook;
                    //StoryBookSaveManager.instance. = sceneToLoad;
                    Singleton.SelectedBook = selectedStoryBook;
                    StartCoroutine(IEClick());
                    clicked = true;
                    isClickable = false;
                }
            }
            catch (TimeUsageException timeUsageException)
            {
                MessageBox.ins.ShowOk(timeUsageException.Message, MessageBox.MsgIcon.msgError, null);
            }

        }

    }

    IEnumerator IEClick()
    {


        //BookshelfManager.ins.PlayBookClick();
        //transform.parent.GetComponent<AudioSource>().PlayOneShot(BookshelfManager.ins.AudClipBookClick);
        BookshelfManager.ins.PlayBookClick();
        imgClickEffect.SetActive(true);
        imgClickEffect.transform.SetParent(transform);
        imgClickEffect.transform.SetLocalXPos(0);
        imgClickEffect.transform.SetLocalYPos(0);
        yield return new WaitForSeconds(1f);

        if (isAssetBundle)
        {
            try
            {
                string url = PlayerPrefs.GetString(assetBunldeUrlKey);
                int version = PlayerPrefs.GetInt(assetBundleUrlVersionKey);

                EmptySceneLoader.ins.loadUrl = url;
                EmptySceneLoader.ins.loadVersion = version;
                EmptySceneLoader.ins.sceneToLoad = sceneToLoad;
                EmptySceneLoader.ins.isAssetBundle = true;

                //this is from bookshelf so
                EmptySceneLoader.ins.unloadUrl = PlayerPrefs.GetString("BookShelf_url_key");
                EmptySceneLoader.ins.unloadVersion = PlayerPrefs.GetInt("BookShelf_version_key");
                EmptySceneLoader.ins.unloadAll = false;

                SceneManager.LoadSceneAsync("empty");
                //LoadSceneFromAssetBundle loader = new LoadSceneFromAssetBundle(url, version);
                //StartCoroutine(loader.IEStreamAssetBundle());
            }
            catch (LoadSceneFromAssetBundleException ex)
            {
                Debug.LogError("The book url key downloaded from assetbundle not found.\n Download try downloading the book again from the launcher.");
            }

        }
        else
        {
            EmptySceneLoader.ins.isAssetBundle = false;
            EmptySceneLoader.ins.sceneToLoad = sceneToLoad;
            SceneManager.LoadSceneAsync("empty");
        }

    }


}
