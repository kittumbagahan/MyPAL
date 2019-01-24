using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ButtonActivity : MonoBehaviour
{

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


   public String SceneToLoad
   {
      get { return sceneToLoad; }
   }

   public Module Mode
   {
      get { return module; }
   }

   void Start()
   {
      animator = GetComponent<Animator> ();
      LoadStarButton ();
      audSrc = GetComponent<AudioSource> ();
   }

   public Material Grayscale { set { grayscale = value; } get { return grayscale; } }

   void LoadStarButton()
   {
      string saveState = "";
      try
      {
         //storyBook = Singleton.SelectedBook;
         storyBook = StoryBookSaveManager.ins.selectedBook;
         index = Read.instance.SceneIndex (storyBook, module, buttonIndex);
         sceneToLoad = Read.instance.SceneName (storyBook, module, buttonIndex);

         DataService.Open ();
         string bookname = storyBook.ToString ();
         string modulename = module.ToString ();



         BookModel book = DataService._connection.Table<BookModel> ().Where (a => a.Description == bookname).FirstOrDefault ();


         ActivityModel activityModel = DataService._connection.Table<ActivityModel> ().Where (
              x => x.BookId == book.Id &&
              x.Description == sceneToLoad &&
              x.Module == modulename &&
              x.Set == index).FirstOrDefault ();



         var studentActivityModel = DataService._connection.Table<StudentActivityModel> ().Where (x =>
               x.SectionId == StoryBookSaveManager.ins.activeSection_id &&
               x.StudentId == StoryBookSaveManager.ins.activeUser_id &&
               x.BookId == book.Id &&
               x.ActivityId == activityModel.Id).FirstOrDefault ();
         //if (!saveState.Equals("0") && saveState != "")
         if (studentActivityModel != null)
         {
            /*change mat*/
            GetComponent<Image> ().material = null;
         }
         else
         {
            /*change mat*/
            GetComponent<Image> ().material = grayscale;
         }
         DataService.Close ();
         //print(StoryBookSaveManager.instance.oldUsername + storyBook + "_" + module + ", " + buttonIndex);
      }
      catch (Exception ex)
      {
         Debug.Log ("IF YOU ARE TESTING BOOK ACTIVITIES WITH OTHER BOOKS ACTIVIY ENABLE THEY WILL RETURN AN ERROR FROM THE QUERY\n" +
             "this will be fixed after all loaded activities are from the book itself.");
         print (ex + "\n" + "THIS BUTTON HAS BEEN DISABLED. TRY REMOVING THE TRY CATCH BLOCK TO SEE WHY.");
         //gameObject.SetActive(false);
         //LINQ activitymodel returns null
         GetComponent<Image> ().material = grayscale;
      }

   }

   public void Click()
   {
      SaveTest.Set = index;
      SaveTest.module = module;
      SaveTest.storyBook = storyBook;
      //print("LOADING " + sceneToLoad);
      //BG_Music.ins.Mute();
      BG_Music.ins.SetToReadingVolume ();
      StartCoroutine (IEClick ());
      //Application.LoadLevel(sceneToLoad);

      /*
       the line below does not work
       */
      //SceneLoader.instance.LoadStr(sceneToLoad);
      AddActivity ();
   }
   IEnumerator IEClick()
   {

      yield return new WaitForSeconds (1f);
      string url = PlayerPrefs.GetString (sceneToLoad + "_url_key");
      if (!"".Equals(url))
      {
         
         int version = PlayerPrefs.GetInt (sceneToLoad + "_version_key");

         EmptySceneLoader.ins.loadUrl = url;
         EmptySceneLoader.ins.loadVersion = version;
         EmptySceneLoader.ins.sceneToLoad = sceneToLoad;
         EmptySceneLoader.ins.isAssetBundle = true;

         //this is from activity selection so
         EmptySceneLoader.ins.unloadUrl = PlayerPrefs.GetString ("ActivitySelection_url_key");
         EmptySceneLoader.ins.unloadVersion = PlayerPrefs.GetInt ("ActivitySelection_version_key");
         EmptySceneLoader.ins.unloadAll = false;

         AssetBundleInfo.ActivityScene.urlKey = sceneToLoad + "_url_key";
         AssetBundleInfo.ActivityScene.versionKey = sceneToLoad + "_version_key";
         AssetBundleInfo.ActivityScene.isAssetBundle = true;
         AssetBundleInfo.ActivityScene.name = sceneToLoad;

         SceneManager.LoadSceneAsync ("empty");
      }
      else
      {
         EmptySceneLoader.ins.isAssetBundle = false;
         print (sceneToLoad + " LOAD THAT!");
         sceneLoader.AsyncLoadStr (sceneToLoad);
      }



   }


   public void RandomShake()
   {
      animator.SetInteger ("index", UnityEngine.Random.Range (0, 3));
   }

   void AddActivity()
   {
      //DataService ds = new DataService ();
      DataService.Open ();
      string _module = module.ToString ();
      string _book = storyBook.ToString ();
      var dup = DataService._connection.Table<ActivityModel> ().Where (x => x.Module == _module &&
         x.Description == sceneToLoad &&
         x.Set == index
      ).FirstOrDefault ();

      if (dup == null)
      {
         var act = new ActivityModel
         {
            BookId = DataService._connection.Table<BookModel> ().Where (x => x.Description == _book).FirstOrDefault ().Id,
            Description = sceneToLoad,
            Module = module.ToString (),
            Set = index
         };
         DataService._connection.Insert (act);
         var a = DataService._connection.Table<BookModel> ().Where (x => x.Description == _book).FirstOrDefault ();
         DataService.Close ();
         Debug.Log (a.ToString ());

      }
      else
      {
         Debug.Log (dup.ToString ());
      }
   }
}
